﻿


Number.prototype.AddZero = function (b, c) {
    var l = (String(b || 10).length - String(this).length) + 1;
    return l > 0 ? new Array(l).join(c || '0') + this : this;
}//to add zero to less than 10,


var d = new Date(),
    localDateTime = [(d.getMonth() + 1).AddZero(),
    d.getDate().AddZero(),
    d.getFullYear()].join('/') + ', ' +
        [d.getHours().AddZero(),
        d.getMinutes().AddZero()].join(':');
var elemStart = document.getElementById("startDate");
var elemEnd = document.getElementById("endDate");
elemStart.value = localDateTime;
elemEnd.value = localDateTime;


var originalPrice = parseFloat(document.getElementById("RoomPrice").innerHTML);
document.getElementById("endDate").addEventListener('change', ValidatePrice);

//$('endDate').change(ValidatePrice());

function ValidatePrice() {
    console.log("HElp me");
    CheckDate();
    let _startDate = new Date(document.getElementById("startDate").value);
    let _endDate = new Date(document.getElementById("endDate").value);
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
    document.getElementById("endDate").onchange = function () {
        let startDate = document.getElementById("startDate").value;
        let endDate = document.getElementById("endDate").value;

        if ((Date.parse(endDate) <= Date.parse(startDate))) {
            alert("End date should be greater than Start date");
            document.getElementById("endDate").value = "";
        }
    };
 };