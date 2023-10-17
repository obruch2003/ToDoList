var deletebutton = document.querySelectorAll('.DownButton');
RemoveUser(deletebutton);

function RemoveUser(deletebutton) {
    deletebutton.forEach(button => {
        button.addEventListener('click', function () {
            var removediv = button.closest('.user');

            fetch("/Main/DeleteFromProject", {
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