$("#button_logout").click(function (e) {
    console.log("clicked");
    e.preventDefault();
    var form = document.getElementById("form_logout");

    Swal.fire({
        title: "Are you sure?",
        text: "click 'logout' if you want to end your session",
        confirmButtonText: "Logout",
        cancelButtonColor: "#d33",
        showCancelButton: true,
        confirmButtonColor: "#08a10b",
        timer: 10000,
    }).then((result) => {
        if (result.isConfirmed) {
            let timerInterval
            Swal.fire({
                title: 'Auto close alert!',
                html: 'I will close in <b></b> milliseconds.',
                timer: 1000,
                timerProgressBar: true,
                didOpen: () => {
                    Swal.showLoading()
                    const b = Swal.getHtmlContainer().querySelector('b')
                    timerInterval = setInterval(() => {
                        b.textContent = Swal.getTimerLeft()
                    }, 100)
                },
                willClose: () => {
                    clearInterval(timerInterval)
                }
            }).then((result) => {
                /* Read more about handling dismissals below */
                if (result.dismiss === Swal.DismissReason.timer) {
                    form.submit()
                }
            })
        } else {
            Swal.fire("Failed when logout", "", "info");
        }
    });
});
