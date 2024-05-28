let currentData = generateDayData();
let tasksChart, focusChart;
let currentPeriod = 'day';

document.addEventListener('DOMContentLoaded', function () {
    populateTable(currentData);
    createCharts(currentData);
});

function selectPeriod(period) {
    currentPeriod = period;

    document.querySelectorAll('.period-buttons button').forEach(button => {
        button.classList.remove('active');
    });
    document.querySelector(`button[onclick="selectPeriod('${period}')"]`).classList.add('active');

    switch (period) {
        case 'day':
            currentData = generateDayData();
            break;
        case 'week':
            currentData = generateWeekData();
            break;
        case 'month':
            currentData = generateMonthData();
            break;
        case 'year':
            currentData = generateYearData();
            break;
    }

    populateTable(currentData);
    updateCharts(currentData);
}

function populateTable(data) {
    const totalTime = data.reduce((sum, stat) => sum + stat.focusTime, 0);
    const totalTasks = data.reduce((sum, stat) => sum + stat.tasksCompleted, 0);
    const averageTime = totalTime / data.length;
    const averageTasks = totalTasks / data.length;

    document.getElementById('totalTime').textContent = (totalTime / 3600).toFixed(2) + ' h.';
    document.getElementById('totalTasks').textContent = totalTasks;
    document.getElementById('averageTime').textContent = (averageTime / 3600).toFixed(2) + ' h.';
    document.getElementById('averageTasks').textContent = averageTasks.toFixed(2);
    document.getElementById('favoriteTime').textContent = findFavoriteTime(data);
    document.getElementById('peakTime').textContent = findPeakTime(data);
}

function findFavoriteTime(data) {
    return '18:00 - 19:00';
}

function findPeakTime(data) {
    if (currentPeriod === 'day') {
        const peakHourIndex = data.reduce((maxIndex, stat, index, array) => stat.focusTime > array[maxIndex].focusTime ? index : maxIndex, 0);
        return data[peakHourIndex].date;
    } else {
        const peakDateIndex = data.reduce((maxIndex, stat, index, array) => stat.focusTime > array[maxIndex].focusTime ? index : maxIndex, 0);
        return new Date(data[peakDateIndex].date).toLocaleDateString();
    }
}

function createCharts(data) {
    const labels = data.map(stat => currentPeriod === 'day' ? stat.date : new Date(stat.date).toLocaleDateString());
    const tasksCompleted = data.map(stat => stat.tasksCompleted);
    const focusTime = data.map(stat => (stat.focusTime / 3600).toFixed(2));

    const averageTasksCompleted = tasksCompleted.reduce((sum, val) => sum + val, 0) / tasksCompleted.length;
    const averageFocusTime = focusTime.reduce((sum, val) => sum + parseFloat(val), 0) / focusTime.length;

    const tasksCtx = document.getElementById('tasksChart').getContext('2d');
    tasksChart = new Chart(tasksCtx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Done tasks',
                    data: tasksCompleted,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                },
                {
                    label: 'Average tasks',
                    data: Array(labels.length).fill(averageTasksCompleted),
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                    borderDash: [5, 5]
                }
            ]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    const focusCtx = document.getElementById('focusChart').getContext('2d');
    focusChart = new Chart(focusCtx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Work time (hours)',
                    data: focusTime,
                    borderColor: 'rgba(153, 102, 255, 1)',
                    borderWidth: 1
                },
                {
                    label: 'Average work time',
                    data: Array(labels.length).fill(averageFocusTime.toFixed(2)),
                    borderColor: 'rgba(255, 159, 64, 1)',
                    borderWidth: 1,
                    borderDash: [5, 5]
                }
            ]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function updateCharts(data) {
    const labels = data.map(stat => currentPeriod === 'day' ? stat.date : new Date(stat.date).toLocaleDateString());
    const tasksCompleted = data.map(stat => stat.tasksCompleted);
    const focusTime = data.map(stat => (stat.focusTime / 3600).toFixed(2));

    const averageTasksCompleted = tasksCompleted.reduce((sum, val) => sum + val, 0) / tasksCompleted.length;
    const averageFocusTime = focusTime.reduce((sum, val) => sum + parseFloat(val), 0) / focusTime.length;

    tasksChart.data.labels = labels;
    tasksChart.data.datasets[0].data = tasksCompleted;
    tasksChart.data.datasets[1].data = Array(labels.length).fill(averageTasksCompleted);
    tasksChart.update();

    focusChart.data.labels = labels;
    focusChart.data.datasets[0].data = focusTime;
    focusChart.data.datasets[1].data = Array(labels.length).fill(averageFocusTime.toFixed(2));
    focusChart.update();
}