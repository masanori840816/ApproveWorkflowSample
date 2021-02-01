using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ApprovementWorkflowSample.Applications
{
    public class ApplicationUser: IdentityUser<int>
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column("user_name")]
        public override string UserName { get; set; } = "";
        [Column("organization")]
        public string? Organization { get; set; }
        [Required]
        [Column("mail")]
        public override string Email { get; set; } = "";
        [Required]
        [Column("password")]
        public override string PasswordHash { get; set; } = "";
        [Required]
        [Column("last_update_date", TypeName = "timestamp with time zone")]
        public DateTime LastUpdateDate { get; set; }
        [NotMapped]
        public override bool EmailConfirmed { get; set; }
        [NotMapped]
        public override string NormalizedUserName {
            get
            {
                return UserName.ToUpper();
            }
            set
            {
                // DO nothing
            }
        }
        [NotMapped]
        public override string NormalizedEmail {
            get
            {
                return Email.ToUpper();
            }
            set
            {
                // Do nothing
            }
        }
        [NotMapped]
        public override bool LockoutEnabled { get; set; }
        [NotMapped]
        public override int AccessFailedCount { get; set; }
        [NotMapped]
        public override string? PhoneNumber { get; set; }
        [NotMapped]
        public override string? ConcurrencyStamp { get; set; }
        [NotMapped]
        public override string? SecurityStamp { get; set; }
        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }
        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }
    }
}