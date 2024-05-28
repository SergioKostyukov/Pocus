function generateDayData() {
    const dayData = [];

    for (let i = 0; i < 10; i++) {
        const hour = i.toString().padStart(2, '0');
        const nextHour = (i + 1).toString().padStart(2, '0');
        const date = `${hour}:00 - ${nextHour}:00`;
        const tasksCompleted = 0;
        const focusTime = 0;

        dayData.push({ date, tasksCompleted, focusTime });
    }

    for (let i = 11; i < 24; i++) {
        const hour = i.toString().padStart(2, '0');
        const nextHour = (i + 1).toString().padStart(2, '0');
        const date = `${hour}:00 - ${nextHour}:00`;
        const tasksCompleted = Math.floor(Math.random() * 5);
        const focusTime = Math.floor(Math.random() * 3600);

        dayData.push({ date, tasksCompleted, focusTime });
    }

    document.getElementById('intevalValue').textContent = `28.05.2024`;

    return dayData;
}

function generateWeekData() {
    const weekData = [];

    const startDate = new Date('2024-05-21');
    const endDate = new Date('2024-05-27');

    for (let date = startDate; date <= endDate; date.setDate(date.getDate() + 1)) {
        const formattedDate = date.toISOString().split('T')[0];
        const tasksCompleted = Math.floor(5 * 4 + Math.random() * 5 * 4);
        const focusTime = Math.floor(3600 * 4 + Math.random() * 3600 * 4);

        weekData.push({ date: formattedDate, tasksCompleted, focusTime });
    }

    document.getElementById('intevalValue').textContent = `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString()}`;

    return weekData;
}

function generateMonthData() {
    const monthData = [];

    const startDate = new Date('2024-05-01');
    const endDate = new Date('2024-05-31');

    for (let date = startDate; date <= endDate; date.setDate(date.getDate() + 1)) {
        const formattedDate = date.toISOString().split('T')[0];
        const tasksCompleted = Math.floor(5 * 15 + Math.random() * 5 * 15);
        const focusTime = Math.floor(3600 * 15 + Math.random() * 3600 * 15);

        monthData.push({ date: formattedDate, tasksCompleted, focusTime });
    }

    document.getElementById('intevalValue').textContent = `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString() }`;

    return monthData;
}

function generateYearData() {
    const yearData = [];

    const startDate = new Date('2024-01-01');
    const endDate = new Date('2024-12-31');

    for (let date = startDate; date <= endDate; date.setMonth(date.getMonth() + 1)) {
        const formattedDate = date.toISOString().split('T')[0];
        const tasksCompleted = Math.floor(5 * 15 + Math.random() * 5 * 15);
        const focusTime = Math.floor(2600 * 15 + Math.random() * 3600 * 15);

        yearData.push({ date: formattedDate, tasksCompleted, focusTime });
    }

    document.getElementById('intevalValue').textContent = `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString()}`;

    return yearData;
}