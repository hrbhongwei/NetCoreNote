using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models
{
    /// <summary>
    /// 记事本
    /// </summary>
    public class Note
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create { get; set; }
        /// <summary>
        /// 类型Id
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// 记事本类型
        /// </summary>
        public NoteType Type { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment { get; set; }
    }
}
