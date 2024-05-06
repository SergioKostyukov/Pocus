let timerValue = Number(sessionStorage.getItem('timerValue')) || 40;

const timerDisplay = document.getElementById("timerDisplay");
const numberBreksElement = document.getElementById('numberBreks');
const breakToggle = document.getElementById("breakToggle");

breakToggle.checked = settingsData.DisableBreaks;
breakToggle.addEventListener("change", handleBreakToggle);

function increaseTimer() {
    timerValue += 5;
    updateTimerDisplay();
}

function decreaseTimer() {
    timerValue -= 5;
    if (timerValue < 0) {
        timerValue = 0;
    }
    updateTimerDisplay();
}

function resetTimerValue() {
    timerValue = 40;
    updateTimerDisplay();
}

function handleBreakToggle() {
    const disableBreaks = this.checked;
    sessionStorage.setItem('disableBreaks', disableBreaks);
    updateBreaksNumber(disableBreaks);
}

function updateTimerDisplay() {
    timerDisplay.innerText = formatTime(timerValue);
    sessionStorage.setItem('timerValue', timerValue);
    updateBreaksNumber();

    function formatTime(seconds) {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;
        return `${minutes}:${remainingSeconds < 10 ? '0' : ''}${remainingSeconds}`;
    }
}

function updateBreaksNumber(disableBreaks) {
    if (!disableBreaks) {
        let work_time = parseFloat(settingsData.WorkTime);
        let break_time = parseFloat(settingsData.BreakTime);
        numberBreksElement.textContent = "Number of breaks: " + Math.floor(timerValue / (work_time + break_time));
    } else {
        numberBreksElement.textContent = "You will have no breaks";
    }
}
