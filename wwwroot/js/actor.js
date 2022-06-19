$("#FullName").on("keyup", function () {
    if ($(this).val() != "") {
        $("#actor_name").html($(this).val());
    } else {
        $("#actor_name").html("Actor Name");
    }
})

$("#ProfilePictureURL").on("keyup", function () {
    if ($(this).val() != "") {
        $("#actor_image").attr("src", $(this).val());
    } else {
        $("#actor_image").attr("src", "https://firebasestorage.googleapis.com/v0/b/coba-firebase-d3a0e.appspot.com/o/default.png?alt=media&token=44d223fb-097b-44a1-a8d5-b188e7f69b91");
    }
})

$("#Bio").on("keyup", function () {
    if ($(this).val() != "") {
        $("#actor_bio").html($(this).val());

    } else {
        $("#actor_bio").html("Not yet....");
    }
})