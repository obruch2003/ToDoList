var check = document.querySelectorAll('input[type="checkbox"]');

check.forEach(button => {
    button.addEventListener('click', function () {
        fetch("/Main/CheckTask", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(button.id)
        })
            .then(response => response.json())
            .then(data => {
            });
    })
});
