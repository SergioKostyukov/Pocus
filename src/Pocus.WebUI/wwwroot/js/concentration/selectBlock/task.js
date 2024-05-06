document.addEventListener("DOMContentLoaded", function () {
    const modalUpdate = document.getElementById("viewTask");

    document.querySelector('.task-sets').addEventListener('click', function (event) {
        const taskBlock = event.target.closest('.task-title-block');
        if (taskBlock) {
            ShowModal(taskBlock);

            modalUpdate.classList.add('active');
        }
    });

    document.addEventListener("click", function (event) {
        if (event.target === modalUpdate) {
            modalUpdate.classList.remove("active");
        }
    });

    async function ShowModal(taskBlock) {
        var id = parseInt(taskBlock.id.replace('block', ''), 10);
        var taskData = await getPlanData(id);

        fillViewModal('viewTaskBlock', taskData);
    }
});

function taskViewTextContainer(viewTaskBlock, objectData) {
    const textContainer = document.createElement('div');
    textContainer.classList.add('text-container');

    if (objectData.text) {
        const objectContentArray = JSON.parse(objectData.text);

        objectContentArray.forEach(objectContent => {
            const doneToggleElement = document.createElement('div');
            doneToggleElement.classList.add('done-toggle');

            const checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.id = 'breakToggle';
            checkbox.checked = objectContent.isDone;
            checkbox.disabled = true;

            const paragraph = document.createElement('p');
            paragraph.contentEditable = false;
            paragraph.textContent = objectContent.text;
            if (checkbox.checked) {
                paragraph.classList.add("line-through");
            }

            doneToggleElement.appendChild(checkbox);
            doneToggleElement.appendChild(paragraph);

            textContainer.appendChild(doneToggleElement);
        });
    }

    viewTaskBlock.appendChild(textContainer);
}

function deactivateModalTask() {
    const modal = document.getElementById("viewTask");
    modal.classList.remove("active");
}

/* ----------------------------- Requests ----------------------------- */
async function getPlanData(id) {
    try {
        const response = await $.ajax({
            url: `https://localhost:7232/Concentration/GetPlanById?id=${id}`,
            type: 'GET',
            contentType: 'application/json'
        });

        if (response.plan != null) {
            return response.plan;
        } else {
            console.log(response.message);
            throw new Error(response.message);
        }
    } catch (error) {
        console.error('Error! ', error);
        alert('Error!', error);
    }
}