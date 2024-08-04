



function btnCart() {
    var quantity = $('#txtqty').val();
    var prodId = $('#product_Prod_Id').val();

    $('#myloaderCart').show();
    $('#cartbtn').hide();

    $.ajax({
        url: '/Product/InsertInCart',
        type: 'POST',
        dataType:'JSON',
        data: { quantity: quantity, prodId: prodId },
        success: function (result) {
            window.location.href = result.url;
        },
        error: function (result) {
            $('#myloaderCart').hide();
            $('#cartbtn').show();
        }
    });
}


$('#forgotPasswordLink').click(function () {
    $('#forgotPasswordModal').modal('show');
});



$('#forgotPasswordForm').submit(function (e) {
    e.preventDefault();
    var email = $('#forgetemail').val();
    $.ajax({
        url: '/Account/ForgetPass',
        type: 'POST',
        data: { email: email },
        success: function (response) {
            
            $('#responseMess').text(response);
            $('#btnSbmt').hide();
        },
        error: function (response) {

            $('#responseMess').text(response);
            $('#btnSbmt').show();
        }
    });
    //$('#forgotPasswordModal').modal('hide');
});



function btnRecover() {
    var Password = $('#InputPassword').val();
    var CPassword = $('#InputCPassword').val();
    var qstring = $('#recoverMessageId').val();

    if (Password !== CPassword) {
        alert("Password and Confirm Password must be the same");
    } else {
        $.ajax({
            url: '/Account/ResetPassword',
            type: 'POST',
            dataType: 'JSON',
            data: { Password: Password, codeid: qstring }, // Use colon instead of equals sign here
            success: function (result) {
                if (result.success) {
                    alert(result.message); // Show success message
                    window.location.href = result.url; // Redirect to the desired URL
                } else {
                    alert(result.message); // Show error message
                }
            },
            error: function (result) {
                // Handle error
            }
        });
    }
}




