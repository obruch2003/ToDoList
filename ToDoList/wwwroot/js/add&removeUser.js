var deletebutton = document.querySelectorAll('.DownButton');
RemoveFromTask(deletebutton);

function RemoveFromTask(deletebutton) {
    deletebutton.forEach(button => {
        button.addEventListener('click', function () {
            
            fetch("/Main/RemoveFromTask", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(button.id)
            })
                .then(response => response.json())
                .then(data => {
                    if (data) {
                        button.className = "UpButton";
                        button.textContent = "Add"
                        var addbutton = document.querySelectorAll('.UpButton');
                        AddToTask(addbutton);
                    }
                });
        })
    });
}

var addbutton = document.querySelectorAll('.UpButton');
AddToTask(addbutton);
function AddToTask(addbutton) {
    addbutton.forEach(button => {
        var idtaskValue = button.getAttribute('data-idtask');

        var task = {
            id: button.id,
            idsprint: idtaskValue
        };
        button.addEventListener('click', function () {
            fetch("/Main/AddToTask", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(task)
            })
                .then(response => response.json())
                .then(data => {
                    if (data) {
                        button.className = "DownButton";
                        button.textContent = "Remove"

                        var deletebutton = document.querySelectorAll('.DownButton');
                        RemoveFromTask(deletebutton);
                    }
                });
        })
    });
}