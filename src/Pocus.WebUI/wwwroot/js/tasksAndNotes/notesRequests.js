async function addNote(blockName) {
    var noteData = GetNoteData(blockName);

    var requestParams = {
        Path: 'AddNote',
        Type: 'POST',
        RequestObject: noteData,
        ErrorMessage: 'Adding Note'
    }

    await serverRequest(requestParams);
}

async function updateData(blockName) {
    var noteData = GetNoteData(blockName);

    var { Title, Text, IsPinned } = noteData;
    var request = {
        Id: parseInt(document.getElementById(blockName).querySelector('h3').id),
        Title,
        Text,
        IsPinned
    };

    console.log(request.IsPinned)

    var requestParams = {
        Path: 'UpdateNote',
        Type: 'PATCH',
        RequestObject: request,
        ErrorMessage: 'Updating Note'
    }

    await serverRequest(requestParams);
}

async function copyNote(NoteName) {
    const noteBlock = document.getElementById(NoteName);

    var requestData = {
        id: parseInt(noteBlock.querySelector('h3').id),
    };

    var requestParams = {
        Path: 'CopyNote',
        Type: 'POST',
        RequestObject: requestData,
        ErrorMessage: 'Copy Note'
    }

    await serverRequest(requestParams);
}

async function updatePin(pin) {
    var requestData = {
        Id: parseInt(pin.id.replace('pin', ''), 10)
    };

    var requestParams = {
        Path: 'UpdateNotePin',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Updating Note pin'
    }

    await serverRequest(requestParams);
}

async function archiveNote(NoteName) {
    const noteBlock = document.getElementById(NoteName);

    UnselectItem(noteBlock.querySelector('h3').id);

    var requestData = {
        Id: parseInt(noteBlock.querySelector('h3').id),
    };

    var requestParams = {
        Path: 'ArchiveNote',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Archiving Note'
    }

    await serverRequest(requestParams);
}

async function deleteNote(NoteName) {
    const noteBlock = document.getElementById(NoteName);

    UnselectItem(noteBlock.querySelector('h3').id);

    var requestData = {
        Id: parseInt(noteBlock.querySelector('h3').id),
    };

    var requestParams = {
        Path: 'DeleteNote',
        Type: 'DELETE',
        RequestObject: requestData,
        ErrorMessage: 'Deleting Note'
    }

    await serverRequest(requestParams);
}

async function serverRequest(request) {
    await $.ajax({
        url: 'https://localhost:7232/Note/' + request.Path,
        type: request.Type,
        contentType: 'application/json',
        data: JSON.stringify(request.RequestObject),
        success: function (data) {
            console.log(data.message);
            window.location.href = '/Note/Get';
        },
        error: function (error) {
            console.error(`Error! ${request.ErrorMessage}:`, error);
            alert(`Error! ${request.ErrorMessage}:`, error);
        }
    });
}

/* ----------------------------- Helpers ----------------------------- */
function GetNoteData(blockName) {
    const noteBlock = document.getElementById(blockName);

    const noteTitle = noteBlock.querySelector('h3').textContent;

    const noteBodyElement = noteBlock.querySelector(".text-container");
    const boteContent = noteBodyElement.querySelector("p").textContent;

    const pinButton = noteBlock.querySelector(".pin-button");
    var isPinned = pinButton.classList.contains("pinned");

    var noteData = {
        Title: noteTitle,
        Text: boteContent,
        IsArchived: false,
        IsPinned: isPinned
    };

    return noteData;
}

function UnselectItem(id) {
    var selectedPlan = localStorage.getItem('selected_note');
    if (selectedPlan == parseInt(id)) {
        localStorage.removeItem('selected_note');
    }
}