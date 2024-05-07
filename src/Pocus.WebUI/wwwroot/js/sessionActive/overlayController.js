document.addEventListener("DOMContentLoaded", function () {
    // Event listener to check if overlay was active on previous session
    window.addEventListener("load", function () {
        ActivateOverlay();
    });

    const ActivateOverlay = async () => {
        InitializeTimerUI();
        await TimerController();
    };

    // Event listener to prompt user before leaving the page if overlay is active
    window.addEventListener('beforeunload', function (e) {
        SaveBlocksData();
        sessionStorage.setItem("timerStatus", timePassed);

        const confirmationMessage = 'You are leaving this page. Are you sure?';

        (e || window.event).returnValue = confirmationMessage;
        return confirmationMessage;
    });

    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            var paragraph = this.parentNode.querySelector('p');

            if (this.checked) {
                paragraph.classList.add("line-through");
            } else {
                paragraph.classList.remove("line-through");
            }
        });
    });
});

function UpdateCompleteTime() {
    var currentCompleteTime = parseInt(localStorage.getItem("complete_time")) || 0;
    var lastcompleteTimeInterval = parseInt(sessionStorage.getItem("timerStatus"));

    localStorage.setItem("complete_time", currentCompleteTime + lastcompleteTimeInterval);
    sessionStorage.setItem("timerStatus", 0);
}

async function SaveBlocksData() {
    await UpdateTask();
    await UpdateNote();
    await UpdateHabits();
}
