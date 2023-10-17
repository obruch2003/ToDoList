var upbutton = document.querySelectorAll('.UpButton');
up(upbutton);

var downbutton = document.querySelectorAll('.DownButton');
down(downbutton);


function down(downbutton) {
    downbutton.forEach(button => {
        button.addEventListener('click', function () {
            fetch("/Main/Down", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(button.id)
            })
                .then(response => response.json())
                .then(data => {
                    if (data) {
                        if (data != null) {
                            button.className = "UpButton";
                            button.textContent = "Up"
                            var parent = button.closest('.user');
                            var text = parent.querySelector('.name');
                            text.textContent = "name: " + data.name + " role: " + data.role;
                            var upbutton = document.querySelectorAll('.UpButton');
                            up(upbutton);
                        }
                    }
                });
        })
    });
}
function up(upbutton) {
    upbutton.forEach(button => {
        button.addEventListener('click', function () {
            var user = button.closest('.user');
            fetch("/Main/Up", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(button.id)
            })
                .then(response => response.json())
                .then(data => {
                    if (data != null) {
                        button.className = "DownButton";
                        button.textContent = "Down"
                        var parent = button.closest('.user');
                        var text = parent.querySelector('.name');
                        text.textContent = "name: " + data.name + " role: " + data.role;
                        var downbutton = document.querySelectorAll('.DownButton');
                        down(downbutton);
                    }
                });
        })
    });
}