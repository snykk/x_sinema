namespace x_sinema.Constans
{
    public class MyFlasher
    {
        public static string flasher(string message, bool berhasil = false, bool gagal = false)
        {
            string status = "status";
            string logo = "logo";
            if (berhasil == true)
            {
                status = "success";
                logo = "check-circle-fill";
            }
            else if (gagal == true)
            {
                status = "danger";
                logo = "exclamation-triangle-fill";
            }

            string result = "<div class='alert alert-" + status + " d-flex justify-content-between align-items-center mt-3' role='alert'><i class='bi bi-" + logo + " me-2' style='font-size:1.5rem'></i><div>" + message + "</div><button type ='button' class='btn-close ms-auto p-2 bd-highlight' data-bs-dismiss='alert' aria-label='Close'></button></div>";

            return result;
        }
    }
}
