using Domain.Dto;
using System.Collections.Generic;

namespace AYCProjectBudgeting.CustomDto
{
    public class OptionGroupDto<TDto>
    {
        public TDto Parent { get; set; }
        public List<TDto> ChildList { get; set; }
    }

}
