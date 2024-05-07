async function UpdateTask() {
    var requestData = {
        id: localStorage.getItem('selected_task'),
        text: GetPlanText('Plan')
    };

    var requestParams = {
        Path: 'UpdatePlanText',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Updating Plan'
    }

    await serverRequest(requestParams);
}

async function UpdateHabits() {
    var requestData = {
        id: localStorage.getItem('selected_habits'),
        text: GetPlanText('Habits')
    };

    var requestParams = {
        Path: 'UpdatePlanText',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Updating Habits'
    }

    await serverRequest(requestParams);
}

function GetPlanText(objectId) {
    const taskBlock = document.getElementById(objectId);

    const doneToggleElements = taskBlock.querySelectorAll(".done-toggle");

    const taskContentArray = [];

    doneToggleElements.forEach(doneToggleElement => {
        const text = doneToggleElement.querySelector("p").textContent;
        const isDone = doneToggleElement.querySelector("#breakToggle").checked;
        taskContentArray.push({ text, isDone });
    });

    return JSON.stringify(taskContentArray);
}

async function UpdateNote() {
    const noteBlock = document.getElementById('Note');

    var requestData = {
        id: localStorage.getItem('selected_note'),
        text: noteBlock.querySelector("p").textContent
    };

    var requestParams = {
        Path: 'UpdateNoteText',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Updating Note'
    }

    await serverRequest(requestParams);
}

async function serverRequest(request) {
    await $.ajax({
        url: 'https://localhost:7232/Concentration/' + request.Path,
        type: request.Type,
        contentType: 'application/json',
        data: JSON.stringify(request.RequestObject),
        success: function (data) {
            console.log(data.message);
        },
        error: function (error) {
            console.error(`Error! ${request.ErrorMessage}:`, error);
        }
    });
}