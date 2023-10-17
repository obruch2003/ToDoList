var deletebutton = document.querySelectorAll('.DeleteButton');
RemoveSprint(deletebutton);

function RemoveSprint(deletebutton) {
    deletebutton.forEach(button => {
        button.addEventListener('click', function () {
            var removediv = button.closest('.sprint');
            fetch("/Main/DeleteSprint", {
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