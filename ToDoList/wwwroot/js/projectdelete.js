var deletebutton = document.querySelectorAll('.DeleteButton');

deletebutton.forEach(button => {
    button.addEventListener('click', function () {
        var removediv = button.closest('.project_container');
        fetch("/Main/DeleteProject", {
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
