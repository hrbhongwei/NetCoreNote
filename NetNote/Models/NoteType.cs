using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models
{
    public class NoteType
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public List<Note> Notes { get; set; }
    }
}
