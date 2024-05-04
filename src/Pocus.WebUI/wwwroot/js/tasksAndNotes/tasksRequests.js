function GetPlanData(blockName) {
    const planBlock = document.getElementById(blockName);
    const planTitle = planBlock.querySelector("#planTitle").textContent;

    const pinButton = planBlock.querySelector(".pin-button");
    var isPinned = pinButton.classList.contains("pinned");

    // Get the content of "done-toggle" elements
    const doneToggleElements = planBlock.querySelectorAll(".done-toggle");

    // Create an array to store text and status
    const planContentArray = [];

    // Iterate over "done-toggle" elements and gather text and status
    doneToggleElements.forEach(doneToggleElement => {
        const text = doneToggleElement.querySelector("p").textContent;
        const isDone = doneToggleElement.querySelector("#breakToggle").checked;
        planContentArray.push({ text, isDone });
    });

    var planData = {
        Title: planTitle,
        Text: JSON.stringify(planContentArray),
        IsArchived: false,
        IsPinned: isPinned
    };

    return planData;
}

/* ----------------------------- Requests ----------------------------- */

// Function to request adding a Plan to the DB
async function addPlan(blockName) {
    var request = GetPlanData(blockName);
    if (planData.Title == "Habits") {
        console.log("Can`t save Plan with title like this");
        alert("Incorrect title, please change it");
        return false;
    }

    try {
        await serverRequest('Plan/AddPlan', 'POST', request);
    } catch (error) {
        console.error('Error adding Plan:', error);
    }

    return true;
}

// Function to request updating a Plan
async function updateData(blockName) {
    var planData = GetPlanData(blockName);

    var request = {
        Id: parseInt(planBlock.querySelector("#planTitle").id),
        Title: planData.Title,
        Text: planData.Text,
        IsPinned: planData.IsPinned
    };

    try {
        await serverRequest('Plan/UpdatePlan', 'PATCH', request);
    } catch (error) {
        console.error('Error updating Plan:', error);
    }

    return true;
}

// Function to request copying a Plan
async function copyPlan(PlanName){
    const planBlock = document.getElementById(PlanName);
    var requestData = {
        request: parseInt(planBlock.querySelector('h3').id),
    };

    try {
        await serverRequest('Plan/CopyPlan', 'POST', requestData);
    } catch (error) {
        console.error('Plan copy error:', error);
    }
}

// Function to request updating the pinned status of a Plan
async function updatePin(pin) {
    var requestData = {
        request: parseInt(pin.id.replace('pin', ''), 10),
    };

    try {
        await serverRequest('Plan/UpdatePlanPin', 'PATCH', requestData);
    } catch (error) {
        console.error('Error updating Plan pin:', error);
    }
}

// Function to request archiving a Plan
async function archivePlan(PlanName){
    const planBlock = document.getElementById(PlanName);
    var requestData = {
        request: parseInt(planBlock.querySelector('h3').id)
    };

     // check if this block selected
     var selectedPlan = localStorage.getItem('selected_plan');
     if (selectedPlan == parseInt(planBlock.querySelector('h3').id)) {
         localStorage.removeItem('selected_plan');
     }

    try {
        await serverRequest('Plan/ArchivePlan', 'PATCH', requestData);
    } catch (error) {
        console.error('Error archiving Plan:', error);
    }
    planBlock.classList.remove("active");
}

// Function for Plan notification
function notificationPlan(PlanName){
}

// Function to request deleting a Plan
async function deletePlan(PlanName){
    const planBlock = document.getElementById(PlanName);
    var requestData = {
        Id: parseInt(planBlock.querySelector('h3').id),
    };

    // check if this block selected
    var selectedPlan = localStorage.getItem('selected_plan');
    if (selectedPlan == parseInt(planBlock.querySelector('h3').id)) {
        localStorage.removeItem('selected_plan');
    }

    try {
        await serverRequest('Plan/DeletePlan', 'DELETE', requestData);
    } catch (error) {
        console.error('Error deleting Plan:', error);
    }
    planBlock.classList.remove("active");
}

// Template function for sending a request
async function serverRequest(path, type, requestObject) {
    $.ajax({
        url: 'https://localhost:7232/' + path,
        type: type,
        contentType: 'application/json',
        data: JSON.stringify(requestObject),
        success: function (data) {
            console.log(data.message);
            window.location.href = '/Plan/Get';
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error('There was a problem with the fetch operation:', errorThrown);
            throw errorThrown;
        }
    });
}