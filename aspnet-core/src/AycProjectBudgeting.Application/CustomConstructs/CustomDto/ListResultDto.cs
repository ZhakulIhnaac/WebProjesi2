using System.Collections.Generic;

namespace AYCProjectBudgeting.CustomConstructs.ListResult
{
    public class ListResultDto<TDto>
    {
        public long totalItemCount { get; set; }
        public List<TDto> itemList { get; set; }
    }

}
