async function addPlan(blockName) {
    var planData = GetPlanData(blockName);

    if (planData.Title == "Habits") {
        console.log("Can`t save Plan with title like this");
        alert("Incorrect title, please change it");
        return;
    }

    var requestParams = {
        Path: 'AddPlan',
        Type: 'POST',
        RequestObject: planData,
        ErrorMessage: 'Adding Plan'
    }

    await serverRequest(requestParams);
}

async function updateData(blockName) {
    var planData = GetPlanData(blockName);

    var { Title, Text, IsArchived, IsPinned } = planData;
    var request = {
        Id: parseInt(document.getElementById(blockName).querySelector('h3').id),
        Title,
        Text,
        IsArchived,
        IsPinned
    };

    var requestParams = {
        Path: 'UpdatePlan',
        Type: 'PATCH',
        RequestObject: request,
        ErrorMessage: 'Updating Plan'
    }

    await serverRequest(requestParams);
}

async function copyPlan(PlanName) {
    const planBlock = document.getElementById(PlanName);

    var requestData = {
        Id: parseInt(planBlock.querySelector('h3').id),
    };

    var requestParams = {
        Path: 'CopyPlan',
        Type: 'POST',
        RequestObject: requestData,
        ErrorMessage: 'Copy Plan'
    }

    await serverRequest(requestParams);
}

async function updatePin(pin) {
    var requestData = {
        Id: parseInt(pin.id.replace('pin', ''), 10),
    };

    var requestParams = {
        Path: 'UpdatePlanPin',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Updating Plan pin'
    }

    await serverRequest(requestParams);
}

async function archivePlan(PlanName) {
    const planBlock = document.getElementById(PlanName);

    UnselectItem(planBlock.querySelector('h3').id);

    var requestData = {
        Id: parseInt(planBlock.querySelector('h3').id)
    };

    var requestParams = {
        Path: 'ArchivePlan',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Archiving Plan'
    }

    await serverRequest(requestParams);
}

async function notificationPlan(PlanName) {
}

async function deletePlan(PlanName) {
    const planBlock = document.getElementById(PlanName);

    UnselectItem(planBlock.querySelector('h3').id);

    var requestData = {
        Id: parseInt(planBlock.querySelector('h3').id),
    };

    var requestParams = {
        Path: 'DeletePlan',
        Type: 'DELETE',
        RequestObject: requestData,
        ErrorMessage: 'Deleting Plan'
    }

    await serverRequest(requestParams);
}

async function serverRequest(request) {
    await $.ajax({
        url: 'https://localhost:7232/Plan/' + request.Path,
        type: request.Type,
        contentType: 'application/json',
        data: JSON.stringify(request.RequestObject),
        success: function (data) {
            console.log(data.message);
            window.location.href = '/Plan/Get';
        },
        error: function (error) {
            console.error(`Error! ${request.ErrorMessage}:`, error);
            alert(`Error! ${request.ErrorMessage}:`, error);
        }
    });
}

/* ----------------------------- Helpers ----------------------------- */
function GetPlanData(blockName) {
    const planBlock = document.getElementById(blockName);

    const planTitle = planBlock.querySelector('h3').textContent;

    const doneToggleElements = planBlock.querySelectorAll(".done-toggle");
    const planContentArray = [];
    doneToggleElements.forEach(doneToggleElement => {
        const text = doneToggleElement.querySelector("p").textContent;
        const isDone = doneToggleElement.querySelector("#breakToggle").checked;
        planContentArray.push({ text, isDone });
    });

    const pinButton = planBlock.querySelector(".pin-button");
    var isPinned = pinButton.classList.contains("pinned");

    var planData = {
        Title: planTitle,
        Text: JSON.stringify(planContentArray),
        IsArchived: false,
        IsPinned: isPinned
    };

    return planData;
}

function UnselectItem(id) {
    var selectedPlan = localStorage.getItem('selected_plan');
    if (selectedPlan == parseInt(id)) {
        localStorage.removeItem('selected_plan');
    }
}