using System.ComponentModel.DataAnnotations;

namespace InspectionAPI
{
    public class Inspection
    {
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Status { get; set; }
      
        [StringLength(20)]
        public string Comments { get; set; }
        
        public int InstructionTypeId { get; set; }

        public InspectionType? InspectionType { get; set; }
    }
}
