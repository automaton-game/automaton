﻿using System.Collections.Generic;

namespace Tools.Documentador.Dtos
{
    public class MethodInfoDto : ItemMemberInfo, IMethodInfoDto
    {
        public ICollection<IItemMemberInfo> Params { get; set; }

        public override string ToString()
        {
            return $"{this.Name}()";
        }
    }
}