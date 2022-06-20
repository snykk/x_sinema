$("#link_checkout").click(function (e) {
    console.log("clicked");
    e.preventDefault();
    const href = $(this).attr("href");

    Swal.fire({
        title: "Are you sure?",
        text: "After this process checkout data will store into order history",
        icon: "question",
        confirmButtonText: "Checkout",
        cancelButtonColor: "#d33",
        showCancelButton: true,
        confirmButtonColor: "#08a10b",
        timer: 10000,
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire("Your action is being processed", "", "success").then((_) => document.location.href = href);
        } else {
            Swal.fire("Action canceled by user", "", "info");
        }
    });
});
