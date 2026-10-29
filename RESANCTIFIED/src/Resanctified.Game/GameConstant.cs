using Microsoft.Xna.Framework;

namespace Resanctified.Game
{
    public static class GameConstant
    {
        public const float MUSIC_VOLUME = 1f;
        public const float SFX_VOLUME = 1f;
        public const float CREDITS_PAGE_TIME = 4f;
        public const float MENU_FADE_TIME = 2f;
        public const float MENU_INGAME_EXIT_FADE_TIME = 1f;
        public const int HIGHSCORES_NB_MAX = 5;
        public const float PIXEL_RATIO_SURVIVAL = 0.008333334f;
        public const float PIXEL_RATIO_STORY = 0.009090909f;
        public const float PIXEL_RATIO_BOSS = 0.01f;
        public const float CAMERA_FOLLOW_SMOOTH_FACTOR_X = 2.5f;
        public const float CAMERA_FOLLOW_SMOOTH_FACTOR_Y = 2f;
        public const float CAMERA_FOLLOW_MAX_SPEED_X = 30f;
        public const float CAMERA_FOLLOW_MAX_SPEED_Y = 30f;
        public const float CAMERA_FOLLOW_OFFSET_X = 0.95f;
        public const float CAMERA_FOLLOW_OFFSET_Y_STORY = 1f;
        public const float CAMERA_POS_X_STORY = 1f;
        public const float CAMERA_POS_Y_STORY = 1f;
        public const float CAMERA_POS_X_BOSS = 1f;
        public const float CAMERA_POS_Y_BOSS = 2f;
        public const float CAMERA_POS_X_SURVIVAL = 1f;
        public const float CAMERA_POS_Y_SURVIVAL = 2f;
        public const float CAMERA_FOLLOW_SMOOTH_FACTOR_X_SURVIVAL = 2.5f;
        public const float CAMERA_FOLLOW_SMOOTH_FACTOR_Y_SURVIVAL = 2.5f;
        public const float CAMERA_FOLLOW_MAX_SPEED_X_SURVIVAL = 30f;
        public const float CAMERA_FOLLOW_MAX_SPEED_Y_SURVIVAL = 30f;
        public const float CAMERA_FOLLOW_OFFSET_X_SURVIVAL = 0.95f;
        public const float CAMERA_FOLLOW_OFFSET_Y_SURVIVAL = 1f;
        public const float CAMERA_BOSS_FOLLOW_SMOOTH_FACTOR_X = 1.5f;
        public const float CAMERA_BOSS_FOLLOW_SMOOTH_FACTOR_Y = 2f;
        public const float CAMERA_BOSS_FOLLOW_OFFSET_X = 0.95f;
        public const float CAMERA_BOSS_FOLLOW_OFFSET_Y = 1f;
        public const float CAMERA_BOSS_FOLLOW_MAX_SPEED_X = 20f;
        public const float CAMERA_BOSS_FOLLOW_MAX_SPEED_Y = 30f;
        public const float CAMERA_BOSS_TARGET_X_MAX = 0.8f;
        public const int DAY_INDEX_START = 1;
        public const int DAY_INDEX_END = 31;
        public const int LEVEL_INDEX_START = 1;
        public const int LEVEL_INDEX_END = 3;
        public const float LEVEL_DEFAULT_LEFT_LIMIT = -1f;
        public const float LEVEL_DEFAULT_CAMERA_LEFT_LIMIT = 0.6f;
        public const float LEVEL_DEFAULT_CAMERA_RIGHT_LIMIT = -1f;
        public const float LEVEL_STORY_CAMERA_RIGHT_LIMIT = -1.4f;
        public const float LEVEL_DEFAULT_END_POS_X = 40f;
        public const float LEVEL_DEFAULT_HERO_POS_X = 0.0f;
        public const float LEVEL_BOSS_LEFT_LIMIT = -0.5f;
        public const float LEVEL_BOSS_RIGHT_LIMIT = 7.5f;
        public const float LEVEL_BOSS_CAMERA_LEFT_LIMIT = 1.4f;
        public const float LEVEL_BOSS_CAMERA_RIGHT_LIMIT = 5.5f;
        public const float LEVEL_BOSS_HERO_POS_X = 3.5f;
        public const float LEVEL_BOSS_FINAL_LEFT_LIMIT = 0.2f;
        public const float LEVEL_SHOP_LEFT_LIMIT = -1f;
        public const float LEVEL_SHOP_RIGHT_LIMIT = 8f;
        public const float LEVEL_SHOP_CAMERA_LEFT_LIMIT = 0.7f;
        public const float LEVEL_SHOP_CAMERA_RIGHT_LIMIT = 6.3f;
        public const float LEVEL_SHOP_HERO_POS_X = 3.8f;
        public const float LEVEL_ENDING_HERO_POS_X = 4f;
        public const float LEVEL_ENDING_DEAD_NUN_POS_X = 5f;
        public const float LEVEL_SHOP_NPC_POS_X = 6f;
        public const float LEVEL_SHOP_NPC_ICON_POS_Y = 1.7f;
        public const float LEVEL_SHOP_EXIT_POS_X = -0.5f;
        public const float LEVEL_SHOW_ARROW_LIMIT = 8f;
        public const float LEVEL_FADE_TIME = 2f;
        public const int MISSION_INDEX_START = 0;
        public const int INFESTATION_LEVEL_MAX = 3;
        public const int DIFFICULTY_HARD_LEVEL_ADD = 10;
        public const float Z_LAYER_HERO = 0.0f;
        public const float Z_LAYER_HERO_SHADOW = -0.2f;
        public const float Z_LAYER_HERO_GUN = -0.1f;
        public const float Z_LAYER_HERO_GUN_FX = 2f;
        public const float Z_LAYER_EXPLOSION_FX = 4f;
        public const float Z_LAYER_BULLET_IMPACT_FX = 3.5f;
        public const float Z_LAYER_ZOMBIE = -1f;
        public const float Z_LAYER_ZOMBIE_SHADOW = -0.2f;
        public const float Z_LAYER_ZOMBIE_ICE = 0.1f;
        public const float Z_LAYER_ZOMBIE_ATTACK = 3f;
        public const float Z_LAYER_ZOMBIE_DEAD = 3.5f;
        public const float Z_LAYER_POTION = 3.6f;
        public const float Z_LAYER_HERO_DEAD = 3.7f;
        public const float Z_LAYER_BOSS = -2f;
        public const float Z_LAYER_BOSS_BALL = 1f;
        public const float Z_LAYER_BOSS_BALL_BROKEN = -1.5f;
        public const float Z_LAYER_BOSS_SHADOW = -0.2f;
        public const float Z_LAYER_BOSS_SHIELD = 1f;
        public const float Z_LAYER_SPIKE = -1.5f;
        public const float Z_LAYER_FALLING_ROCK = 3.8f;
        public const float Z_LAYER_NPC_SHOP = -2f;
        public const float Z_LAYER_BOSS_NECRO_DEAD_NUN = -2.5f;
        public const int HERO_HIT_POINTS_BASE = 100;
        public const int HERO_HIT_POINTS_LEVEL_ADD = 50;
        public const int HERO_MANA_POINTS_BASE = 100;
        public const int HERO_MANA_POINTS_LEVEL_ADD = 50;
        public const int HERO_SLASH_DAMAGE_BASE = 15;
        public const int HERO_SLASH_AIR_DAMAGE_BASE = 50;
        public const int HERO_SLASH_DAMAGE_LEVEL_ADD = 9;
        public const float HERO_SLASH_MANA_DRAIN_BASE = 0.15f;
        public const float HERO_SLASH_MANA_DRAIN_LEVEL_ADD = 0.06f;
        public const float HERO_SLASH_ATTACK_OFFSET = -0.1f;
        public const float HERO_INVULNERABILITY_TIME = 2f;
        public const float HERO_GRAVITY = 20f;
        public const float HERO_WALK_ACCELERATION = 5f;
        public const float HERO_WALK_SPEED_MIN = 0.8f;
        public const float HERO_WALK_SPEED_MAX = 2f;
        public const float HERO_SLIDE_ACCELERATION = -5f;
        public const float HERO_AIR_CONTROL_SPEED = 4f;
        public const float HERO_JUMP_VELOCITY_Y = 8f;
        public const float HERO_JUMP_VELOCITY_X_FACTOR = 0.8f;
        public const float HERO_AIR_FINISHING_Y_MIN = 1f;
        public const float HERO_AIR_FINISHING_VELOCITY_Y = -6f;
        public const float HERO_SLASH_IMPULSE_WAIT_TIME = 0.1f;
        public const float HERO_SLASH_BASE_VELOCITY_X = 0.8f;
        public const float HERO_SLASH_WALK_VELOCITY_X = 2.8f;
        public const float HERO_SLASH_WALK_ACCELERATION_X = -6f;
        public const float HERO_WALK_ANIMATION_SPEED_MIN = 0.8f;
        public const float HERO_WALK_ANIMATION_SPEED_MAX = 1f;
        public const float HERO_PUSH_BACK_AUTO_TIME = 3f;
        public const float HERO_PUSH_BACK_RADIUS = 1f;
        public const float HERO_PUSH_BACK_RADIUS_SQUARED = 1f;
        public const float HERO_PUSH_BACK_MIN_SHAKE_TIME = 0.0f;
        public const float HERO_BLINK_TIME = 0.04f;
        public const float HERO_DEAD_TIME = 0.5f;
        public const float HERO_SHADOW_MIN_SCALE = 0.4f;
        public const float HERO_SHADOW_MAX_SCALE = 1f;
        public const float HERO_SHADOW_MAX_HEIGHT = 1.4f;
        public const int SLASH_2_UNLOCK_AT_UPGRADE_LVL = 2;
        public const int SLASH_3_UNLOCK_AT_UPGRADE_LVL = 3;
        public const float ZOMBIE_HIT_FX_TIME = 0.5f;
        public const int ZOMBIE_HIT_FX_RED_MAX = 50;
        public const int ZOMBIE_STORY_MISSION_STRENGTH_FACTOR = 1;
        public const int ZOMBIE_STORY_INFESTATION_STRENGTH_FACTOR = 4;
        public const int ZOMBIE_POINTS_NORMAL = 10;
        public const int ZOMBIE_POINTS_GRABBER = 20;
        public const int ZOMBIE_POINTS_ARMORED_NORMAL = 30;
        public const int ZOMBIE_POINTS_ARMORED_GRABBER = 40;
        public const int ZOMBIE_HIT_POINTS_NORMAL_MIN = 10;
        public const int ZOMBIE_HIT_POINTS_NORMAL_MAX = 180;
        public const int ZOMBIE_HIT_POINTS_NORMAL_LEVEL_ADD = 6;
        public const int ZOMBIE_HIT_POINTS_GRABBER_MIN = 20;
        public const int ZOMBIE_HIT_POINTS_GRABBER_MAX = 240;
        public const int ZOMBIE_HIT_POINTS_GRABBER_LEVEL_ADD = 8;
        public const int ZOMBIE_ARMORED_HIT_POINTS_NORMAL_MIN = 10;
        public const int ZOMBIE_ARMORED_HIT_POINTS_NORMAL_MAX = 180;
        public const int ZOMBIE_ARMORED_HIT_POINTS_NORMAL_LEVEL_ADD = 6;
        public const int ZOMBIE_ARMORED_HIT_POINTS_GRABBER_MIN = 20;
        public const int ZOMBIE_ARMORED_HIT_POINTS_GRABBER_MAX = 240;
        public const int ZOMBIE_ARMORED_HIT_POINTS_GRABBER_LEVEL_ADD = 8;
        public const float ZOMBIE_ARMORED_RESIST_SLASH = 0.6f;
        public const float ZOMBIE_ARMORED_RESIST_BULLET = 0.6f;
        public const float ZOMBIE_ARMORED_RESIST_EXPLODE = 0.5f;
        public const float ZOMBIE_ARMORED_RESIST_FIRE = 0.8f;
        public const float ZOMBIE_ARMORED_RESIST_ICE = 0.9f;
        public const float ZOMBIE_ARMORED_RESIST_HOLY = 0.8f;
        public const int ZOMBIE_ARMORED_RESIST_BULLET_SHIELD = 30;
        public const int ZOMBIE_SLASH_DMG_MIN = 10;
        public const int ZOMBIE_SLASH_DMG_LEVEL_ADD = 3;
        public const int ZOMBIE_SLASH_DMG_MAX = 100;
        public const int ZOMBIE_ARMORED_SLASH_DMG_MIN = 15;
        public const int ZOMBIE_ARMORED_SLASH_DMG_LEVEL_ADD = 4;
        public const int ZOMBIE_ARMORED_SLASH_DMG_MAX = 150;
        public const int ZOMBIE_GRAB_DMG_MIN = 8;
        public const float ZOMBIE_GRAB_DMG_LEVEL_ADD = 1.5f;
        public const int ZOMBIE_GRAB_DMG_MAX = 50;
        public const float ZOMBIE_GRAB_DMG_TIME = 0.5f;
        public const int ZOMBIE_ARMORED_SPAWN_NIGHT_START = 2;
        public const int ZOMBIE_ARMORED_SPAWN_RATIO_MIN = 10;
        public const int ZOMBIE_ARMORED_SPAWN_RATIO_LEVEL_ADD = 3;
        public const int ZOMBIE_ARMORED_SPAWN_RATIO_MAX = 50;
        public const float ZOMBIE_SPAWN_TIME_SWARM_DELTA = 0.2f;
        public const float ZOMBIE_SPAWN_TIME_MIN = 3f;
        public const float ZOMBIE_SPAWN_TIME_LEVEL_SUB = 0.07f;
        public const float ZOMBIE_SPAWN_TIME_LEVEL_SUB_EVENING = 0.2f;
        public const float ZOMBIE_SPAWN_TIME_LEVEL_SUB_NIGHT = 0.4f;
        public const float ZOMBIE_SPAWN_TIME_LEVEL_SUB_INFESTATION = 0.2f;
        public const float ZOMBIE_SPAWN_TIME_MAX = 4f;
        public const float ZOMBIE_SPAWN_TIME_OVERKILL_ADD = 0.1f;
        public const float ZOMBIE_SPAWN_DISTANCE_MIN = 1f;
        public const float ZOMBIE_SPAWN_DISTANCE_NEAR_MAX = 6.7f;
        public const float ZOMBIE_SPAWN_DISTANCE_MAX = 9f;
        public const int ZOMBIE_SPAWN_FRONT_RATIO = 80;
        public const int ZOMBIE_SPAWN_GRABBER_RATIO_MIN = 10;
        public const int ZOMBIE_SPAWN_GRABBER_RATIO_LEVEL_ADD = 2;
        public const int ZOMBIE_SPAWN_GRABBER_RATIO_MAX = 30;
        public const float ZOMBIE_SPAWN_TUTORIAL_POS_MIN = -2f;
        public const float ZOMBIE_SPAWN_TUTORIAL_POS_MAX = -3f;
        public const float ZOMBIE_UNSPAWN_DISTANCE = 13f;
        public const float ZOMBIE_UNSPAWN_DISTANCE_SQUARED = 169f;
        public const float ZOMBIE_DETECTION_DISTANCE = 0.5f;
        public const float ZOMBIE_DETECTION_DISTANCE_SQUARED = 0.25f;
        public const float ZOMBIE_ARMORED_DETECTION_DISTANCE = 0.6f;
        public const float ZOMBIE_ARMORED_DETECTION_DISTANCE_SQUARED = 0.36f;
        public const float ZOMBIE_DETECTION_DISTANCE_STAND_MIN = 0.1f;
        public const float ZOMBIE_DETECTION_DISTANCE_WALK_MIN = 0.2f;
        public const float ZOMBIE_ATTACK_RATE = 1f;
        public const float ZOMBIE_WALK_SPEED = 0.8f;
        public const float ZOMBIE_GRAB_FRONT_VELOCITY_X = 1f;
        public const float ZOMBIE_PUSH_BACK_VELOCITY_X = 2.8f;
        public const float ZOMBIE_AIRSLASH_PUSH_BACK_VELOCITY_X = 1f;
        public const float ZOMBIE_EXPLOSION_PUSH_BACK_VELOCITY_X = 2f;
        public const float ZOMBIE_DEAD_UNSPAWN_TIME = 6f;
        public const float ZOMBIE_UNSPAWN_BLINK_TIME = 1f;
        public const float ZOMBIE_BLINK_TIME = 0.05f;
        public const int ZOMBIE_GRUNT_PROBABILITY = 50;
        public const float ZOMBIE_GRUNT_TIME_MIN = 3f;
        public const float ZOMBIE_GRUNT_TIME_MAX = 6f;
        public const float ZOMBIE_UNFREEZE_RATIO = 0.2f;
        public const int ZOMBIE_SPLASHES_BLOOD_PROBABILITY = 40;
        public const float ZOMBIE_SHADOW_MIN_SCALE = 0.4f;
        public const float ZOMBIE_SHADOW_MAX_SCALE = 1f;
        public const float ZOMBIE_SHADOW_MAX_HEIGHT = 1.4f;
        public const int ZOMBIE_ARMORED_NB_MAX = 20;
        public const int ZOMBIE_NB_MAX = 55;
        public const int ZOMBIE_TOKILL_NB_TUTORIAL = 20;
        public const int ZOMBIE_TOKILL_NB_STORY_TOWN = 10;
        public const int ZOMBIE_TOKILL_NB_STORY_FOREST = 12;
        public const int ZOMBIE_TOKILL_NB_STORY_GRAVEYARD = 15;
        public const int ZOMBIE_TOKILL_NB_STORY_EVENING_ADD = 2;
        public const int ZOMBIE_TOKILL_NB_STORY_NIGHT_ADD = 4;
        public const int ZOMBIE_TOKILL_NB_STORY_INFESTATION_ADD = 8;
        public const int BOSS_BUTCHER_POINTS = 5000;
        public const int BOSS_NECRO1_POINTS = 8000;
        public const int BOSS_NECRO2_POINTS = 10000;
        public const int BOSS_BUTCHER_HARD_HIT_POINTS_ADD = 1500;
        public const int BOSS_NECRO1_HARD_HIT_POINTS_ADD = 500;
        public const int BOSS_NECRO2_HARD_HIT_POINTS_ADD = 1500;
        public const int BOSS_HARD_DMG_ADD = 40;
        public const int BOSS_INFESTATION_HIT_POINTS_ADD = 500;
        public const int BOSS_INFESTATION_DMG_ADD = 10;
        public const int BOSS_BUTCHER_HIT_POINTS = 3000;
        public const int BOSS_BUTCHER_SLASH_DMG = 70;
        public const float BOSS_BUTCHER_SHADOW_OFFSET_X = 0.1f;
        public const float BOSS_BUTCHER_SHADOW_SCALE_X = 2f;
        public const float BOSS_BUTCHER_SHADOW_SCALE_Y = 1.6f;
        public const float BOSS_BUTCHER_THROW_TIME = 4f;
        public const int BOSS_BUTCHER_DAMAGE_ANGRY = 300;
        public const int BOSS_BUTCHER_DAMAGE_BREAK = 1500;
        public const float BOSS_BUTCHER_WALK_SPEED = 1f;
        public const float BOSS_BUTCHER_WALK_SPEED2 = 1.2f;
        public const float BOSS_POS_X_LEFT = 0.5f;
        public const float BOSS_POS_X_RIGHT = 6.5f;
        public const float BOSS_DETECTION_DISTANCE = 1.2f;
        public const float BOSS_DETECTION_DISTANCE_SQUARED = 1.44f;
        public const float BOSS_TARGET_DISTANCE = 0.5f;
        public const float ZOMBIE_BOSS_LVL_SPAWN_DISTANCE_MIN = 0.0f;
        public const float ZOMBIE_BOSS_LVL_SPAWN_DISTANCE_MIN_RELATIVE = 1.5f;
        public const float ZOMBIE_BOSS_LVL_SPAWN_DISTANCE_MAX_RELATIVE = 5.5f;
        public const int BOSS_NECRO_FORM1_HIT_POINTS = 1500;
        public const int BOSS_NECRO_FORM2_HIT_POINTS = 5000;
        public const int BOSS_NECRO_FORM1_BLAST_DMG = 80;
        public const int BOSS_NECRO_FORM2_BLAST_DMG = 100;
        public const float BOSS_NECRO_FORM1_RESIST_SLASH = 0.0f;
        public const float BOSS_NECRO_FORM1_RESIST_BULLET = 1f;
        public const float BOSS_NECRO_FORM1_RESIST_EXPLODE = 1f;
        public const float BOSS_NECRO_FORM1_RESIST_FIRE = 1f;
        public const float BOSS_NECRO_FORM1_RESIST_ICE = 1f;
        public const float BOSS_NECRO_FORM1_RESIST_HOLY = 0.0f;
        public const float BOSS_NECRO_FORM2_RESIST_SLASH = 0.0f;
        public const float BOSS_NECRO_FORM2_RESIST_BULLET = 0.0f;
        public const float BOSS_NECRO_FORM2_RESIST_EXPLODE = 0.0f;
        public const float BOSS_NECRO_FORM2_RESIST_FIRE = 0.0f;
        public const float BOSS_NECRO_FORM2_RESIST_ICE = 0.0f;
        public const float BOSS_NECRO_FORM2_RESIST_HOLY = 1f;
        public const float BOSS_NECRO_SPELL_TIME = 4f;
        public const float BOSS_NECRO_EARTHQUAKE_TIME = 9f;
        public const float BOSS_NECRO_INVOQUING_TIME = 4f;
        public const int BOSS_NECRO_INVOQUING_ZOMBIE_NB = 8;
        public const float BOSS_NECRO_VULNERABLE_TIME = 3f;
        public const float BOSS_NECRO1_SHIELD_INVOQUE_OFFSET = 0.3f;
        public const float BOSS_NECRO2_SHIELD_INVOQUE_OFFSET = 0.5f;
        public const float BOSS_NECRO_SHIELD_CENTER_OFFSET_Y = 1f;
        public const float BOSS_NECRO_SHIELD_RADIUS = 1.2f;
        public const float BOSS_NECRO_SHIELD_RADIUS_SQUARED = 1.44f;
        public const float BOSS_NECRO_SHIELD_PUSH_BACK_VEL_X = -4f;
        public const float BOSS_NECRO_SHIELD_PUSH_BACK_ACC_X = 8f;
        public const int BOSS_NECRO_ORB_HIT_POINTS = 500;
        public const int BOSS_NECRO_ORB_HIT_POINTS_HARD = 800;
        public const float BOSS_NECRO_MANA_DRAIN = 10f;
        public const float BOSS_NECRO_MANA_DRAIN_HARD = 15f;
        public const float BOSS_NECRO_ORB_POS_X = 0.0f;
        public const float BOSS_NECRO_ORB_NEXT_VULNERABLE_TIME = 10f;
        public const float BOSS_NECRO_ORB_VULNERABLE_TIME = 5f;
        public const float BOSS_NECRO_ORB_RESTORING_TIME = 20f;
        public const float BOSS_NECRO_ORB_SHIELD_SCALE = 0.8f;
        public const float BOSS_NECRO_ORB_SHIELD_CENTER_OFFSET_Y = 0.9f;
        public const float BOSS_NECRO_ORB_SHIELD_RADIUS = 1f;
        public const float BOSS_NECRO_ORB_SHIELD_RADIUS_SQUARED = 1f;
        public const float BOSS_NECRO_ORB_SHIELD_PUSH_BACK_VEL_X = 3f;
        public const float BOSS_NECRO_ORB_SHIELD_PUSH_BACK_ACC_X = -8f;
        public const float BOSS_NECRO_DEAD_NUN_OFFSET_X = 2f;
        public const float BOSS_NECRO_DEAD_NUN_OFFSET_Y = 0.5f;
        public const float GUN_RAYCAST_OFFSET_X = 0.0f;
        public const float GUN_RAYCAST_OFFSET_Y = 0.7f;
        public const float GUN_RAYCAST_DISTANCE_MAX = 10f;
        public const int GUN_BASE_DAMAGE = 10;
        public const int GUN_LEVEL_ADD_DAMAGE = 5;
        public const int GUN_BASE_MANA_CONSUMPTION = 1;
        public const float GUN_LEVEL_ADD_MANA_CONSUMPTION = 0.5f;
        public const float GUN_COOLDOWN = 0.2f;
        public const float GUN_RANGE = 5f;
        public const int MACHINEGUN_BASE_DAMAGE = 10;
        public const int MACHINEGUN_LEVEL_ADD_DAMAGE = 10;
        public const int MACHINEGUN_BASE_MANA_CONSUMPTION = 1;
        public const float MACHINEGUN_LEVEL_ADD_MANA_CONSUMPTION = 0.5f;
        public const float MACHINEGUN_COOLDOWN = 0.1f;
        public const int EXPLOSIVEGUN_BASE_DAMAGE = 80;
        public const int EXPLOSIVEGUN_LEVEL_ADD_DAMAGE = 35;
        public const int EXPLOSIVEGUN_BASE_MANA_CONSUMPTION = 20;
        public const float EXPLOSIVEGUN_LEVEL_ADD_MANA_CONSUMPTION = 8f;
        public const float EXPLOSIVEGUN_COOLDOWN = 0.3f;
        public const float EXPLOSIVEGUN_EXPLOSION_RADIUS = 1.5f;
        public const float EXPLOSIVEGUN_EXPLOSION_RADIUS_SQUARED = 2.25f;
        public const float EXPLOSIVEGUN_RANGE = 4f;
        public const int FLAMETHROWER_BASE_DAMAGE = 6;
        public const int FLAMETHROWER_LEVEL_ADD_DAMAGE = 2;
        public const int FLAMETHROWER_BASE_MANA_CONSUMPTION = 2;
        public const float FLAMETHROWER_LEVEL_ADD_MANA_CONSUMPTION = 0.8f;
        public const float FLAMETHROWER_COOLDOWN = 0.1f;
        public const float FLAMETHROWER_RANGE = 4f;
        public const float FLAMETHROWER_RANGE_SQUARED = 16f;
        public const int ICEGUN_BASE_DAMAGE = 10;
        public const int ICEGUN_LEVEL_ADD_DAMAGE = 5;
        public const int ICEGUN_BASE_MANA_CONSUMPTION = 2;
        public const float ICEGUN_LEVEL_ADD_MANA_CONSUMPTION = 0.5f;
        public const float ICEGUN_BASE_FREEZE_TIME = 2f;
        public const float ICEGUN_LEVEL_ADD_FREEZE_TIME = 1f;
        public const float ICEGUN_COOLDOWN = 0.1f;
        public const float ICEGUN_RANGE = 2.5f;
        public const float ICEGUN_RANGE_SQUARED = 6.25f;
        public const float ICEGUN_FREEZE_LIFE_FLOOR = 0.25f;
        public const float ICEGUN_FREEZE_RATIO = 0.3f;
        public const int HOLYGUN_BASE_DAMAGE = 35;
        public const int HOLYGUN_LEVEL_ADD_DAMAGE = 10;
        public const int HOLYGUN_BASE_MANA_CONSUMPTION = 6;
        public const float HOLYGUN_LEVEL_ADD_MANA_CONSUMPTION = 2f;
        public const float HOLYGUN_COOLDOWN = 0.1f;
        public const float HOLYGUN_RANGE = 6f;
        public const float HOLYGUN_RANGE_SQUARED = 36f;
        public const int EXPLOSION_FX_MAX = 8;
        public const int IMPACT_FX_MAX = 8;
        public const int SPIKE_NB = 8;
        public const float SPIKE_HERO_OFFSET_X = -1f;
        public const float SPIKE_OFFSET_X = 0.7f;
        public const float SPIKE_ENABLE_TIME = 0.5f;
        public const float SPIKE_DISABLE_TIME = 1.5f;
        public const float SPIKE_POS_MAX = 5.5f;
        public const int SPIKE_DMG = 100;
        public const int ROCK_NB = 4;
        public const int ROCK_DMG = 100;
        public const float ROCK_GRAVITY = 9f;
        public const float ROCK_POS_Y_START = 3f;
        public const float ROCK_POS_X_MIN = 1f;
        public const float ROCK_POS_X_MAX = 5f;
        public const float ROCK_TIME = 2f;
        public const int GUN_COST_LEVEL1 = 1;
        public const int GUN_COST_LEVEL2 = 20;
        public const int GUN_COST_LEVEL3 = 30;
        public const int GUN_COST_LEVEL4 = 50;
        public const int GUN_COST_LEVEL5 = 80;
        public const int MACHINEGUN_COST_LEVEL1 = 30;
        public const int MACHINEGUN_COST_LEVEL2 = 50;
        public const int MACHINEGUN_COST_LEVEL3 = 80;
        public const int MACHINEGUN_COST_LEVEL4 = 100;
        public const int MACHINEGUN_COST_LEVEL5 = 120;
        public const int EXPLOSIVEGUN_COST_LEVEL1 = 75;
        public const int EXPLOSIVEGUN_COST_LEVEL2 = 100;
        public const int EXPLOSIVEGUN_COST_LEVEL3 = 125;
        public const int EXPLOSIVEGUN_COST_LEVEL4 = 150;
        public const int EXPLOSIVEGUN_COST_LEVEL5 = 200;
        public const int FLAMETHROWER_COST_LEVEL1 = 60;
        public const int FLAMETHROWER_COST_LEVEL2 = 80;
        public const int FLAMETHROWER_COST_LEVEL3 = 100;
        public const int FLAMETHROWER_COST_LEVEL4 = 120;
        public const int FLAMETHROWER_COST_LEVEL5 = 150;
        public const int ICEGUN_COST_LEVEL1 = 60;
        public const int ICEGUN_COST_LEVEL2 = 80;
        public const int ICEGUN_COST_LEVEL3 = 100;
        public const int ICEGUN_COST_LEVEL4 = 120;
        public const int ICEGUN_COST_LEVEL5 = 150;
        public const int HOLYGUN_COST_LEVEL1 = 120;
        public const int HOLYGUN_COST_LEVEL2 = 160;
        public const int HOLYGUN_COST_LEVEL3 = 200;
        public const int HOLYGUN_COST_LEVEL4 = 250;
        public const int HOLYGUN_COST_LEVEL5 = 300;
        public const int SLASHDAMAGE_COST_LEVEL1 = 10;
        public const int SLASHDAMAGE_COST_LEVEL2 = 25;
        public const int SLASHDAMAGE_COST_LEVEL3 = 50;
        public const int SLASHDAMAGE_COST_LEVEL4 = 75;
        public const int SLASHDAMAGE_COST_LEVEL5 = 100;
        public const int SLASHMANADRAIN_COST_LEVEL1 = 10;
        public const int SLASHMANADRAIN_COST_LEVEL2 = 25;
        public const int SLASHMANADRAIN_COST_LEVEL3 = 50;
        public const int SLASHMANADRAIN_COST_LEVEL4 = 75;
        public const int SLASHMANADRAIN_COST_LEVEL5 = 100;
        public const int HITPOINTS_COST_LEVEL1 = 10;
        public const int HITPOINTS_COST_LEVEL2 = 20;
        public const int HITPOINTS_COST_LEVEL3 = 40;
        public const int HITPOINTS_COST_LEVEL4 = 60;
        public const int HITPOINTS_COST_LEVEL5 = 80;
        public const int MANAPOINTS_COST_LEVEL1 = 10;
        public const int MANAPOINTS_COST_LEVEL2 = 20;
        public const int MANAPOINTS_COST_LEVEL3 = 40;
        public const int MANAPOINTS_COST_LEVEL4 = 60;
        public const int MANAPOINTS_COST_LEVEL5 = 80;
        public const int AIRSLASH_COST_LEVEL1 = 40;
        public const int AIRSLASH_COST_LEVEL2 = 60;
        public const int AIRSLASH_COST_LEVEL3 = 80;
        public const int AIRSLASH_COST_LEVEL4 = 100;
        public const int AIRSLASH_COST_LEVEL5 = 120;
        public const int POTION_MAX = 4;
        public const int POTION_ZOMBIES_TO_KILL_MIN = 20;
        public const int POTION_ZOMBIES_TO_KILL_MAX = 30;
        public const int POTION_ZOMBIES_TO_KILL_MIN_HARD = 30;
        public const int POTION_ZOMBIES_TO_KILL_MAX_HARD = 40;
        public const float POTION_OFFSET_X = 1.5f;
        public const float POTION_HEAL_RATIO = 0.5f;
        public const float POTION_TIME = 20f;
        public const float POTION_BLINKING_TIME = 2f;
        public const float POTION_BLINK_TIME = 0.05f;
        public const float POTION_PICKUP_DISTANCE = 0.5f;
        public const float POTION_PICKUP_DISTANCE_SQUARED = 0.25f;
        public const float INPUTMANAGER_DEADZONE_X = 0.1f;
        public const float INPUTMANAGER_DEADZONE_Y = 0.3f;
        public static Color MENU_TEXT_COLOR = new Color((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color MENU_TEXT_OUTLINE_COLOR = new Color(50, 0, 0, (int)byte.MaxValue);
        public static Color MENU_TEXT_SELECTED_COLOR = new Color(200, 0, 0, (int)byte.MaxValue);
        public static Color MENU_TEXT_HELP_SELECTED_COLOR = new Color((int)byte.MaxValue, 100, 0, (int)byte.MaxValue);
        public static Color MENU_TEXT_NOTAVAILABLE_COLOR = new Color(128, 128, 128, (int)byte.MaxValue);
        public static Color CREDITS_TEXT_COLOR = new Color((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color CREDITS_TEXT_OUTLINE_COLOR = new Color(50, 0, 0, (int)byte.MaxValue);
        public static Color CREDITS_TEXT_TITLE1_COLOR = new Color(0, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color CREDITS_TEXT_TITLE2_COLOR = new Color((int)byte.MaxValue, 100, 0, (int)byte.MaxValue);
        public static Color INGAME_BLACKBACKGROUND_COLOR = new Color(0, 0, 0, 200);
        public static Color OUTGAME_BLACKBACKGROUND_COLOR = new Color(0, 0, 0, 200);
        public static Color TEXTBOX_NUN1_NAME_COLOR = new Color(220, 140, 120, (int)byte.MaxValue);
        public static Color TEXTBOX_NUN2_NAME_COLOR = new Color(220, 110, 110, (int)byte.MaxValue);
        public static Color TEXTBOX_PRIEST_NAME_COLOR = new Color(0, 50, 200, (int)byte.MaxValue);
        public static Color TEXTBOX_BOSS_NAME_COLOR = new Color(200, 0, 0, (int)byte.MaxValue);
        public static Color TEXTBOX_TEXT_COLOR = new Color(50, 50, 50, (int)byte.MaxValue);
        public static Color TEXTBOX_TEXT_CHOICE_COLOR = new Color(100, 100, 100, (int)byte.MaxValue);
        public static Color SHIELD_INVULNERABLE_COLOR = new Color(0, 0, 0, (int)byte.MaxValue);
        public static Color SHIELD_BULLET_COLOR = new Color((int)byte.MaxValue, 162, 0, (int)byte.MaxValue);
        public static Color SHIELD_FIRE_COLOR = new Color((int)byte.MaxValue, 0, 0, (int)byte.MaxValue);
        public static Color SHIELD_ICE_COLOR = new Color(0, 180, (int)byte.MaxValue, (int)byte.MaxValue);
        public static Color SHIELD_HOLY_COLOR = new Color((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        public static float SHIELD_COLOR_TRANSITION_TIME = 0.7f;
        public static int ZOMBIE_ALIVE_NB_BOSS = 8;
        public static int ZOMBIE_ALIVE_NB_TUTORIAL = 4;
        public static int ZOMBIE_ALIVE_NB_STORY_TOWN = 8;
        public static int ZOMBIE_ALIVE_NB_STORY_FOREST = 9;
        public static int ZOMBIE_ALIVE_NB_STORY_GRAVEYARD = 10;
        public static int ZOMBIE_ALIVE_NB_STORY_INFESTATION_ADD = 2;
        public static int[] ZOMBIE_ALIVE_NB_ARRAY = new int[31]
        {
      8,
      8,
      8,
      8,
      8,
      9,
      9,
      9,
      9,
      10,
      10,
      10,
      10,
      11,
      11,
      11,
      12,
      12,
      12,
      13,
      13,
      13,
      13,
      14,
      14,
      14,
      14,
      15,
      15,
      15,
      15
        };
        public static int[] ZOMBIE_TOKILL_NB_ARRAY = new int[31]
        {
      20,
      30,
      30,
      30,
      40,
      40,
      40,
      40,
      50,
      50,
      50,
      50,
      50,
      50,
      60,
      60,
      60,
      60,
      60,
      80,
      80,
      80,
      80,
      80,
      100,
      100,
      100,
      100,
      100,
      150,
      150
        };
        public static int[] ZOMBIE_SWARM_NB_ARRAY = new int[62]
        {
      1,
      3,
      1,
      3,
      1,
      3,
      1,
      3,
      1,
      3,
      1,
      3,
      1,
      3,
      1,
      3,
      1,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      3,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      4,
      2,
      5,
      2,
      5
        };
        public static int[] ZOMBIE_STORY_SWARM_NB_ARRAY = new int[6]
        {
      1,
      3,
      1,
      4,
      2,
      4
        };
    }
}
