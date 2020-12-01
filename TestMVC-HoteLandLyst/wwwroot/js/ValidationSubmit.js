document.addEventListener('DOMContentLoaded', ready);


function ready() {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
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


var originalPrice = parseFloat(document.getElementById("RoomPrice").innerHTML);
document.getElementById("endDate").onchange =



    function ValidatePrice() {

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