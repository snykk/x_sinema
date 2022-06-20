$("#Name").on("keyup", function () {
    if ($(this).val() != "") {
        $("#company_name").html($(this).val());
    } else {
        $("#company_name").html("Company Name");
    }
})

$("#Logo").on("keyup", function () {
    if ($(this).val() != "") {
        $("#actor_image").attr("src", $(this).val());
    } else {
        $("#actor_image").attr("src", "https://firebasestorage.googleapis.com/v0/b/coba-firebase-d3a0e.appspot.com/o/default.png?alt=media&token=44d223fb-097b-44a1-a8d5-b188e7f69b91");
    }
})

$("#Description").on("keyup", function () {
    if ($(this).val() != "") {
        $("#company_description").html($(this).val());

    } else {
        $("#company_description").html("Not yet....");
    }
})

$("#button_update_company").click(function (e) {
    console.log("clicked");
    e.preventDefault();
    var form = document.getElementById("form_edit_company");

    Swal.fire({
        title: "Are you sure?",
        text: "After this process company data will be update",
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


$("#button_delete_company").click(function (e) {
    console.log("clicked");
    e.preventDefault();
    var form = document.getElementById("form_delete_company");

    Swal.fire({
        title: "Are you sure?",
        text: "After this process company data will be deleted",
        icon: "question",
        confirmButtonText: "Deleted",
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
