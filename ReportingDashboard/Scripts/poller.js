function callMe(url) {
    $.ajax(
        {
            type: "GET",
            url: url,
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
                $("#tblCheckIn tbody").html("");
   
                $.each(data, function (index, value) {
                    var time = moment.utc(value.cTime).local().format("L LT");
                    var localTime = moment(time).format("HH:mm");
                    var row = $("<tr><td>" + value.username + "</td><td>" + localTime + "</td><td>" + value.InOrOut + "</td></tr>");
                    $("#tblCheckIn tbody").append(row);

                });
            },
            error: function (msg) {
                alert(msg.responseText);

            }
        });


}





