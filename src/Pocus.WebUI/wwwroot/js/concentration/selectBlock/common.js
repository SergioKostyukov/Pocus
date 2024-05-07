function fillViewModal(viewObjectBlockName, objectData) {
    const viewObjectBlock = document.getElementById(viewObjectBlockName);

    viewObjectBlock.innerHTML = '';

    const objTitle = document.createElement('h3');
    objTitle.textContent = objectData.title;
    objTitle.id = objectData.id;
    objTitle.contentEditable = false;
    viewObjectBlock.appendChild(objTitle);

    const divider = document.createElement('hr');
    viewObjectBlock.appendChild(divider);

    if (viewObjectBlock.id == "viewTaskBlock") {
        taskViewTextContainer(viewObjectBlock, objectData);

        // check if this block selected
        var selectedTask = localStorage.getItem('selected_task');
        if (selectedTask == objectData.id) {
            addSelectButton(viewObjectBlock, `unselect('${objectData.id}', 'task')`, 'remove')
        } else {
            addSelectButton(viewObjectBlock, `select('${objectData.id}', 'task')`)
        }
    } else {
        noteViewTextContainer(viewObjectBlock, objectData);

        // check if this block selected
        var selectedNote = localStorage.getItem('selected_note');
        if (selectedNote == objectData.id) {
            addSelectButton(viewObjectBlock, `unselect('${objectData.id}', 'note')`, 'remove')
        } else {
            addSelectButton(viewObjectBlock, `select('${objectData.id}', 'note')`)
        }
    }
}

function addSelectButton(viewObjectBlock, command, image_ = 'add') {
    const button = document.createElement('button');
    button.classList.add('action-button');
    const image = document.createElement('img');
    image.src = `/images/${image_}.png`;
    image.alt = 'copy';
    button.setAttribute('onclick', command);
    if (image_ == 'add') {
        button.setAttribute('title', 'Select to session')
    } else {
        button.setAttribute('title', 'Unselect from session')
    }
    button.appendChild(image);
    viewObjectBlock.appendChild(button);
}

function HighlightSelectedObjects() {
    var selectedTask = localStorage.getItem('selected_task');
    if (selectedTask != null) {
        const obj = document.getElementById(`block${selectedTask}`);
        obj.classList.add('highlighted');
    }

    var selectedNote = localStorage.getItem('selected_note');
    if (selectedNote != null) {
        const obj = document.getElementById(`block${selectedNote}`);
        obj.classList.add('highlighted');
    }
}

function select(ObjectId, type) {
    // check if there are already selected task
    var selectedObject = localStorage.getItem(`selected_${type}`);
    if (selectedObject !== null) {
        unselect(selectedObject, type);
    }

    const objBlock = document.getElementById(`block${ObjectId}`);

    objBlock.classList.add('highlighted');

    localStorage.setItem(`selected_${type}`, ObjectId);

    setObjectSelectBlock(type);

    if (type === 'task') {
        deactivateModalTask();
    } else {
        deactivateModalNote();
    }
}

function unselect(ObjectId, type) {
    const objBlock = document.getElementById(`block${ObjectId}`);

    objBlock.classList.remove('highlighted');

    localStorage.removeItem(`selected_${type}`);

    setObjectSelectBlock(type);

    if (type === 'task') {
        deactivateModalTask();
    } else {
        deactivateModalNote();
    }
}

function setHabits() {
    if (!settingsData.IgnoreHabits) {
        if (localStorage.getItem('selected_habits')) {
            localStorage.removeItem('selected_habits');
        } else {
            localStorage.setItem('selected_habits', HabitsId);
        }

        setObjectSelectBlock('habits');
    } else {
        alert("Oops, maybe you are ignoring habits(\nChange this option in settings");
        return;
    }
}

function setSelectBlocksImages() {
    setObjectSelectBlock('task');
    setObjectSelectBlock('note');
    setObjectSelectBlock('habits');
}

function setObjectSelectBlock(name) {
    if (name === 'habits' && settingsData.IgnoreHabits) {
        localStorage.removeItem('selected_habits');
    }

    var selectedTask = localStorage.getItem(`selected_${name}`);
    const selectTaskButton = document.getElementById(`select-${name}`);
    if (selectedTask !== null) {
        selectTaskButton.querySelector('img').src = '/images/select_on.png';
    } else {
        selectTaskButton.querySelector('img').src = '/images/select_off.png';
    }
}