document.addEventListener("DOMContentLoaded", function () {
    const modalUpdate = document.getElementById("viewNote");

    document.querySelector('.note-sets').addEventListener('click', function (event) {
        const noteBlock = event.target.closest('.note-title-block');
        if (noteBlock) {
            ShowModal(noteBlock);

            modalUpdate.classList.add('active');
        }
    });

    document.addEventListener("click", function (event) {
        if (event.target === modalUpdate) {
            modalUpdate.classList.remove("active");
        }
    });

    async function ShowModal(noteBlock) {
        var id = parseInt(noteBlock.id.replace('block', ''), 10);
        var noteData = await getNoteData(id);

        fillViewModal('viewNoteBlock', noteData);
    }
});

function noteViewTextContainer(viewNoteBlock, objectData) {
    const textContainer = document.createElement('div');
    textContainer.classList.add('text-container');

    const paragraph = document.createElement('p');
    paragraph.textContent = objectData.text;
    paragraph.contentEditable = false;

    textContainer.appendChild(paragraph);

    viewNoteBlock.appendChild(textContainer);
}

function deactivateModalNote(){
    const modal = document.getElementById("viewNote");
    modal.classList.remove("active");
}

/*  ----------------------------- Requests ----------------------------- */
async function getNoteData(id) {
    try {
        const response = await $.ajax({
            url: `https://localhost:7232/Concentration/GetNoteById?id=${id}`,
            type: 'GET',
            contentType: 'application/json'
        });

        if (response.note != null) {
            return response.note;
        } else {
            console.log(response.message);
            throw new Error(response.message);
        }
    } catch (error) {
        console.error('Error! ', error);
        alert('Error!', error);
    }
}