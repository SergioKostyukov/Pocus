async function unArchivePlan(PlanId) {
    var requestData = {
        Id: PlanId,
    };

    var requestParams = {
        Path: 'Plan/ArchivePlan',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Unarchiving plan'
    }

    await serverRequest(requestParams);
}

async function unArchiveNote(NoteId) {
    var requestData = {
        Id: NoteId
    };

    var requestParams = {
        Path: 'Note/ArchiveNote',
        Type: 'PATCH',
        RequestObject: requestData,
        ErrorMessage: 'Unarchiving note'
    }

    await serverRequest(requestParams);
}

async function deletePlan(PlanId) {
    var requestData = {
        Id: PlanId
    };

    var requestParams = {
        Path: 'Plan/DeletePlan',
        Type: 'DELETE',
        RequestObject: requestData,
        ErrorMessage: 'Deleting plan'
    }

    await serverRequest(requestParams);
}

async function deleteNote(NoteId) {
    var requestData = {
        Id: NoteId
    };

    var requestParams = {
        Path: 'Note/DeleteNote',
        Type: 'DELETE',
        RequestObject: requestData,
        ErrorMessage: 'Deleting note'
    }

    await serverRequest(requestParams);
}

async function serverRequest(request) {
    await $.ajax({
        url: 'https://localhost:7232/' + request.Path,
        type: request.Type,
        contentType: 'application/json',
        data: JSON.stringify(request.RequestObject),
        success: function (data) {
            console.log(data.message);
            window.location.href = '/Archive/Get';
        },
        error: function (error) {
            console.error(`Error! ${request.ErrorMessage}:`, error);
            alert(`Error! ${request.ErrorMessage}:`, error);
        }
    });
}