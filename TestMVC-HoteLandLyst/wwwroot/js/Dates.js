Number.prototype.AddZero = function (b, c) {
    var l = (String(b || 10).length - String(this).length) + 1;
    return l > 0 ? new Array(l).join(c || '0') + this : this;
}//to add zero to less than 10,

GetUnavailableDates();

var d = new Date(),
    localDateTime = [(d.getMonth() + 1).AddZero(),
    d.getDate().AddZero(),
    d.getFullYear()].join('/') + ', ' +
        [d.getHours().AddZero(),
        d.getMinutes().AddZero()].join(':');
var elemStart = document.getElementById("StartDate");
var elemEnd = document.getElementById("EndDate");
elemStart.value = localDateTime;
elemEnd.value = localDateTime;


var originalPrice = parseFloat(document.getElementById("RoomPrice").innerHTML);
//document.getElementById("EndDate").addEventListener('change', ValidatePrice);

//$('EndDate').change(ValidatePrice());

function ValidatePrice() {
    CheckDate();
    let _startDate = new Date(document.getElementById("StartDate").value);
    let _endDate = new Date(document.getElementById("EndDate").value);
    let Difference_In_Time = _endDate.getTime() - _startDate.getTime();
    // To calculate the no. of days between two dates 
    let Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);

    if (Difference_In_Days >= 7) {
        let price = parseFloat(document.getElementById("RoomPrice").innerHTML);
        price *= 0.9;
        document.getElementById("RoomPrice").innerHTML = price;
    }
    else if (parseFloat(document.getElementById("RoomPrice").innerHTML) != originalPrice) {
        document.getElementById("RoomPrice").innerHTML /= 0.9;
    }

};

function CheckDate() {
    document.getElementById("EndDate").onchange = function () {
        let startDate = document.getElementById("StartDate").value;
        let endDate = document.getElementById("EndDate").value;

        if ((Date.parse(endDate) <= Date.parse(startDate))) {
            alert("End date should be greater than Start date");
            document.getElementById("EndDate").value = "";
        }
    };
};


    function GetUnavailableDates() {
        var url = window.location.origin + "/RoomDetails/GetUnavailableDates";
        //var number = $('#roomNumber').val();
        var number = 101;
        var dates;
        $.get(url, { roomNumber: number }, function (data) {
            dates = data;
        });
    }