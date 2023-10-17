var deletebutton = document.querySelectorAll('.DeleteButton');
RemoveTask(deletebutton);

function RemoveTask(deletebutton) {
    deletebutton.forEach(button => {
        button.addEventListener('click', function () {
            var removediv = button.closest('.task');

            fetch("/Main/DeleteTask", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(button.id)
            })
                .then(response => response.json())
                .then(data => {
                    if (data) {
                        removediv.remove();
                    }
                });
        })
    });
}