using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get; private set;}

    public Transform canvas_Fixed;
    public Transform canvas_Window;
    public Transform canvas_Popup;
    
    public Player player;
    public Inventory inventory;
    
    // CurrentWindow는 현재 열려 있는 Window
    // LastOrDefault는 딕셔너리에 저장된 왼도우 중 가장 마지막에 추가된 윈도우를 반환
    public BaseWindow CurrentWindow => _windowUI.Count > 0 ? _windowUI.Values.LastOrDefault() : null;
    
    // 호출 시 타입을 지정해 해당 타입의 윈도우 반환
    public T GetWindow<T>() where T : BaseWindow // T가 반드시 BaseWindow를 상속받아야 함
    {
        // 현재 열려있는 window를 저장하는 딕셔너리를 순환하며 해당 타입<T>의 window가 열려있는지 확인
        foreach (var window in _windowUI.Values)
        {
            // 타입이 T인 윈도우를 찾으면 즉시 반환, 만약 여러 개(코드 상 문제로 인해)가 존재하더라도 가장 먼저 발견되는 윈도우를 반환
            if (window is T tWindow)
                return tWindow;
        }
        return null;
    }
    
    // 고정된 FixedUI는 여러 UI가 동시에 존재하기 때문에 List로 관리
    private readonly List<BaseFixed> _fixedUIs = new();
    // WindowUI는 한번에 하나씩만 열리고, 키로 빠르게 찾을 수 있어 딕셔너리로 관리
    private readonly Dictionary<UIType, BaseWindow> _windowUI = new();
    // PupopUI는 여러 팝업 UI가 겹칠 수 있고, 가장 마지막에 열린 팝업이 먼저 닫히는 구조이기 때문에 후입선출 구조인 Stack 사용
    private readonly Stack<BasePopup> _popupUIs = new();
    
    private UIPool _pool = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    
    
    // FixedUI 활성화 메서드
    // 추가적인 정보가 필요할 경우 param으로 전달
    // 호출시 param을 따로 입력해주지 않으면 자동으로 null이 할당
    public BaseFixed OpenFixedUI(UIType type, OpenParam param = null)
    {
        // GetUI로 생성 시, BaseUI 타입으로 생성되기 때문에 BaseFixed 타입으로 수정
        BaseFixed Fixed = (BaseFixed)_pool.GetUI(type, canvas_Fixed); 
        Fixed.OnOpen(param);
        _fixedUIs.Add(Fixed); // 고정된 UI는 한번에 여러 UI가 존재할 수 있음
        Fixed.gameObject.SetActive(true);
        return Fixed;
    }
    
    // FixedUI 비활성화 메서드
    // 특정 FixedUI를 비활성화
    // public BaseFixed CloseFixedUI(UIType type)
    // {
    //     
    // }
    
    
    // WindowUI 활성화 메서드
    // 추가적인 정보가 필요할 경우 param으로 전달
    // 호출 시 param을 따로 입력해주지 않으면 자동으로 null이 할당
    public BaseWindow OpenWindowUI(UIType type, OpenParam param = null)
    {
        BaseWindow UI = (BaseWindow)_pool.GetUI(type, canvas_Window);
        UI.OnOpen(param);
        UI.gameObject.SetActive(true);  // 기존 UI는 비활성화 해주는 기능 필요
        _windowUI[type] = UI;  // WindowUI는 한번에 하나의 UI만 활성화 가능
        return UI;
    }

    // WindowUI 비활성화 메서드
    // WindowUI는 한번에 하나만 활성화
    // _windowUI에 UIType type이 존재하면 _pool로 되돌리고 _windowUI에서 삭제
    public void CloseWindowUI(UIType type)
    {
        if (_windowUI.TryGetValue(type, out BaseWindow UI))
        {
            UI.OnClose();
            _pool.ReturnUI(type,UI);
            _windowUI.Remove(type);
        }
    }
    
    
    
    // WindowUI 스위치 메서드
    public BaseWindow SwitchWindowUI(UIType type, OpenParam param = null)
    {
        // 모든 WindowUI 비활성화
        foreach (var windowUI in _windowUI.Values)
        {
            windowUI.OnClose();
            windowUI.gameObject.SetActive(false);
        }

        // 이미 생성되어 있는 UI가 있다면 그대로 사용, 없다면 풀에서 가져옴, 대부분의 경우 그대로 사용
        BaseWindow Window;
        if (_windowUI.TryGetValue(type, out Window))
        {
            // 기존 UI를 그대로 사용
        }
        else
        {
            // UI가 생성되어 있지 않다면 풀에서 가져옴
            Window = (BaseWindow)_pool.GetUI(type, canvas_Window);
            _windowUI[type] = Window;
        }
        Window.OnOpen(param);
        Window.gameObject.SetActive(true);

        return Window;
    }
    
    // PopupUI 활성화 메서드
    // 추가적인 정보가 필요할 경우 param으로 전달
    // 호출시 param을 따로 입력해주지 않으면 자동으로 null이 할당
    public BasePopup OpenPopupUI(UIType type, OpenParam param = null)
    {
        BasePopup popup = (BasePopup)_pool.GetUI(type, canvas_Popup);
        popup.OnOpen(param);
        _popupUIs.Push(popup);   // _popupUIs은 스택구조를 갖기 때문에 Push를 해준다
        popup.gameObject.SetActive(true);
        return popup;
    }

    // PopupUI 비활성화 메서드
    // Popup은 가장 위에 있는 Popup부터 꺼야함
    // 선입후출 구조를 갖기 때문에 가장 위에 있는 Popup부터 제거하도록 Pop 사용
    public void CloseTopPopupUI()
    {
        // Pop은 스택이 비어있을 경우 예외가 발생하기 때문에 TryPop 사용
        if (_popupUIs.TryPop(out BasePopup popup))
        {
            popup.OnClose();
            _pool.ReturnUI(popup.UIType, popup);
        }
    }

    public void OnOpenMainWindow()
    {
        canvas_Window.gameObject.SetActive(true);
    }

    public void OnCloseMainWindow()
    {
        canvas_Window.gameObject.SetActive(false);
    }
    
    
}
