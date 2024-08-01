using System;

namespace Sexy.TodLib
{
    internal static class TodLibConstants
    {
        public const int MAX_EFFECTS_PER_ATTACHMENT = 16;

        public const int TICKS_PER_SECOND = 100;

        public const int REANIMATOR_LOAD_TASK_FACTOR = 0;

        public const int MAX_TRACK_NODES = 10;

        public const int MAX_REANIM_IMAGES = 64;

        public const int RENDER_GROUP_HIDDEN = -1;

        public const int RENDER_GROUP_NORMAL = 0;

        public const float DEFAULT_FIELD_PLACEHOLDER = -10000f;

        public const int NO_BASE_POSE = -2;

        public const int MAX_FOLEY_VARIATIONS = 10;

        public const int MAX_FOLEY_INSTANCES = 8;

        public const int MAX_FOLEY_TYPES = 110;

        public const int LAWN_PARTICLE_AVE_MS_TO_LOAD = 6;

        public const int MAX_PARTICLE_FIELDS = 5;

        public const int MAX_HESITATION_MESSAGE_SIZE = 256;

        public const int MAX_HESITATION_BUFFER_SIZE = 262144;

        public const int MAX_TRAIL_POINTS = 20;

        public const int MAX_TRAIL_TRIANGLES = (MAX_TRAIL_POINTS - 1) * 2;
    }
}
