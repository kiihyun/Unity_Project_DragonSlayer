using UnityEngine;

public static class Constants
{
    public static class Interaction
    {
        public const string DEFAULT_INTERACT_TEXT = "E를 눌러 상호작용";
        public const string SAVE_POINT_INTERACT_TEXT = "저장하기";
        public const string KEY_LOCK_DOOR_INTERACT_TEXT = "열쇠가 필요해";
        public const string STAGE_LOCK_DOOR_INTERACT_TEXT = "아직 들어갈 이유가 없어";

        public const float ITEM_GET_UI_INTERVAL = 0.7f;

        public const float MOVING_PLATFORM_INTERVAL = 0.001f;
        public const float MOVING_PLATFORM_ARRIVE_THRESHOLD = 0.01f;

        public const float DOOR_FADEOUT_DURATION = 0.5f;
        public const float DOOR_FADEIN_DURATION = 1f;
        public const float DOOR_MOVE_DURATION = 0.5f;

    }

    public static class Trap
    {
        public const float TRAP_CHEST_EXPLODE_DESTROY_DURATION = 0.8f;
        public const float BEAM_DAMAGE_INTERVAL = 0.1f;
        public const float BEAM_DAMAGE = 1f;
        public const float FIRE_DAMAGE_INTERVAL = 2f;
        public const float FIRE_DAMAGE = 10f;
        public const float GEAR_DAMAGE_INTERVAL = 0.5f;
        public const float GEAR_DAMAGE = 5f;
        public const float LAVA_DAMAGE_INTERVAL = 0.1f;
        public const float LAVA_DAMAGE = 1f;
    }
}
