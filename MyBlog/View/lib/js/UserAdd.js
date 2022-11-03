function addUser() {
    console.log("add")
    var name = $("#InputDisplayName").val();
    var email = $("#InputEmail").val();
    var address = $("#InputAddress").val();
    var phone = $("#InputPhone").val();
    
    var sendInfo = {
        displayName: name,
        email: email,
        phone: phone,
        address:address
    };
    
    $.ajax({
        url: "http://localhost:5114/api/user",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
          },
        dataType: "jsonp",
        data: sendInfo,
        success: function (msg) {
            if (msg) {
                alert("Success");
                location.reload(true);
            } else {
                alert("Cannot add to list !");
            }
        }
    });
}
