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
                $.each(data,
                    function (index, value) {
                        var hours = value.cTime.Hours < 10 ? "0" + value.cTime.Hours : value.cTime.Hours;
                        var minutes = value.cTime.Minutes < 10 ? "0" + value.cTime.Minutes : value.cTime.Minutes;

                        var time = hours + ":" + minutes;
                        var row = $("<tr><td>" +
                            value.username +
                            "</td><td>" +
                            time +
                            "</td><td>" +
                            value.InOrOut +
                            "</td></tr>");
                        $("#tblCheckIn tbody").append(row);
                    });             
            },
            error: function (msg) {
                alert(msg.responseText);

            }
        });


}





