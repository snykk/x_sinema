$("#button_update_movie").click(function (e) {
    console.log("clicked");
    e.preventDefault();
    var form = document.getElementById("form_edit_movie");


    Swal.fire({
        title: "Are you sure?",
        text: "After this process movie data will be update",
        icon: "question",
        confirmButtonText: "Update",
        cancelButtonColor: "#d33",
        showCancelButton: true,
        confirmButtonColor: "#08a10b",
        timer: 10000,
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire("Your action is being processed", "", "success").then((_) => form.submit());
        } else {
            Swal.fire("Action canceled by admin", "", "info");
        }
    });
});
