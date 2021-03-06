﻿using Chilicki.Commline.Domain.Enums.Extensions;
using Chilicki.Commline.Domain.Resources;

namespace Chilicki.Commline.Domain.Enums
{
    public enum LineType
    {
        [LocalizedDescription("Shared_Bus", typeof(CommlineResources))]
        Bus,
        [LocalizedDescription("Shared_Tram", typeof(CommlineResources))]
        Tram
    }
}
