//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CS481_Hub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BLOG
    {
        [Key]
        public int BLOG_ID { get; set; }
        public string USER_ID { get; set; }
        public string BLOG_TEXT { get; set; }
        public System.DateTime CREATE_DT { get; set; }
        public System.DateTime UPDATE_DT { get; set; }
        public string VOID_IND { get; set; }
    }
}
