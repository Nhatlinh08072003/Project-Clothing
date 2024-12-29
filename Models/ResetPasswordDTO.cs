using System.ComponentModel.DataAnnotations;

public class ResetPasswordDTO
{
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
    [Display(Name = "Mật khẩu hiện tại")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
    [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 6)]
    [Display(Name = "Mật khẩu mới")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu mới")]
    [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    [Display(Name = "Xác nhận mật khẩu mới")]
    public string ConfirmPassword { get; set; }
}