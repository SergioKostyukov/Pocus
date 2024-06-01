const generateUniqueId = () => 'toggle_' + Math.random().toString(36).substring(2, 11);

// Toggle pin activation/deactivation
function togglePin(pinID) {
    const pinButton = document.getElementById(pinID);
    const isPinned = pinButton.classList.toggle("pinned"); // Adds or removes the "pinned" class

    const img = pinButton.querySelector('img');
    if (isPinned) {
        img.style.opacity = 1;
    } else {
        img.style.removeProperty('opacity');
    }
}

// Send a request to update the pin state
function saveTogglePin(pinID) {
    togglePin(pinID);

    const pinButton = document.getElementById(pinID);

    updatePin(pinButton);
}

// Function triggered when the save button is clicked
function saveUpdate(blockName) {
    const modal = document.getElementById(blockName);

    if (updateData(blockName)) {
        modal.classList.remove("active");
    }
}

// ------------------------- Generation of modal window elements -------------------------

// Function to fill the update modal window
function fillUpdateModal(updateObjectBlock, objectBlock) {
    // Clear the content of the update modal window before updating
    updateObjectBlock.innerHTML = '';

    // Add the object title
    // Get data about the object from the selected block
    const objTitle = document.createElement('h3');
    const title = objectBlock.querySelector('h3').textContent;
    objTitle.textContent = title;
    const objectId = parseInt(objectBlock.id.replace('block', ''), 10);
    objTitle.id = objectId;
    updateObjectBlock.appendChild(objTitle);

    if (title != "Habits") {
        objTitle.contentEditable = true;
    } else {
        objTitle.contentEditable = false;
    }

    // Add the "pin" button
    const pinButton = document.createElement('button');
    pinButton.classList.add('action-button', 'pin-button');
    const existpinButton = objectBlock.querySelector(".pin-button");
    const isPinned = existpinButton.classList.contains("pinned");
    if (isPinned) {
        pinButton.classList.add('pinned');
    }
    const pinImage = document.createElement('img');
    pinImage.src = '/images/pin.png';
    pinImage.alt = 'pin';
    pinButton.id = 'updatePin' + objectId;
    pinButton.setAttribute('onclick', `togglePin('${pinButton.id}')`);
    pinButton.appendChild(pinImage);
    updateObjectBlock.appendChild(pinButton);

    // Add a separator
    const divider = document.createElement('hr');
    updateObjectBlock.appendChild(divider);

    fillUpdateTextContainer(updateObjectBlock, objectBlock);

    if (updateObjectBlock.id == "updateNoteBlock") {
        addCopyButton(updateObjectBlock, `copyNote('updateNote')`)
        addDeleteButton(updateObjectBlock, `deleteNote('updateNote')`)
        addArchiveButton(updateObjectBlock, `archiveNote('updateNote')`)
        addSaveButton(updateObjectBlock, `saveUpdate('updateNote')`)
    } else {
        addNotificationBlock(updateObjectBlock);
        if (title != "Habits") {
            addCopyButton(updateObjectBlock, `copyPlan('updateTask')`)
            addDeleteButton(updateObjectBlock, `deletePlan('updateTask')`)
            addArchiveButton(updateObjectBlock, `archivePlan('updateTask')`)
        } else {
            addResetCheckboxButton(updateObjectBlock, `resetCheckboxValues('updateTask')`);
        }
        addSaveButton(updateObjectBlock, `saveUpdate('updateTask')`)
    }
}

function addNotificationBlock(updateObjectBlock) {
    const formGroup = document.createElement('div');
    formGroup.classList.add('form-group');

    const label = document.createElement('label');
    label.setAttribute('for', 'taskDate');
    label.textContent = 'Deadline:';

    const input = document.createElement('input');
    input.type = 'date';
    input.id = 'taskDate';
    input.classList.add('form-control');
    input.style.width = '50%';

    formGroup.appendChild(label);
    formGroup.appendChild(input);

    updateObjectBlock.appendChild(formGroup);
}

// Add the "copy" button
function addCopyButton(updateObjectBlock, command) {
    const copyButton = document.createElement('button');
    copyButton.classList.add('action-button', 'first-button');
    copyButton.setAttribute('title', 'Copy');
    const copyImage = document.createElement('img');
    copyImage.src = '/images/copy.png';
    copyImage.alt = 'copy';
    copyButton.setAttribute('onclick', command);
    copyButton.appendChild(copyImage);
    updateObjectBlock.appendChild(copyButton);
}

// Add the "archive" button
function addArchiveButton(updateObjectBlock, command) {
    const archiveButton = document.createElement('button');
    archiveButton.classList.add('action-button', 'second-button');
    archiveButton.setAttribute('title', 'Archive');
    const archiveImage = document.createElement('img');
    archiveImage.src = '/images/folder.png';
    archiveImage.alt = 'archive';
    archiveButton.setAttribute('onclick', command);
    archiveButton.appendChild(archiveImage);
    updateObjectBlock.appendChild(archiveButton);
}

// Add the "delete" button
function addDeleteButton(updateObjectBlock, command) {
    const deleteButton = document.createElement('button');
    deleteButton.classList.add('action-button', 'third-button');
    deleteButton.setAttribute('title', 'Delete');
    const deleteImage = document.createElement('img');
    deleteImage.src = '/images/delete.png';
    deleteImage.alt = 'delete';
    deleteButton.setAttribute('onclick', command);
    deleteButton.appendChild(deleteImage);
    updateObjectBlock.appendChild(deleteButton);
}

// Add the "Reset checkbox" button
function addResetCheckboxButton(updateObjectBlock, command) {
    const resetButton = document.createElement('button');
    resetButton.setAttribute('title', 'Reset');
    resetButton.classList.add('action-button', 'first-button');
    const resetImage = document.createElement('img');
    resetImage.src = '/images/reset.png';
    resetImage.alt = 'reset';
    resetButton.setAttribute('onclick', command);
    resetButton.appendChild(resetImage);
    updateObjectBlock.appendChild(resetButton);
}

// Add the "Save Changes" button
function addSaveButton(updateObjectBlock, command) {
    const saveChangesButton = document.createElement('button');
    saveChangesButton.classList.add('save-task-button');
    saveChangesButton.setAttribute('onclick', command);
    saveChangesButton.textContent = 'Save Changes';
    updateObjectBlock.appendChild(saveChangesButton);
}
