var uploadForm = document.querySelector('.addfile');
uploadForm.addEventListener('submit', function (event) {
    event.preventDefault();

    var formData = new FormData(uploadForm);
    formData.append('idtask', uploadForm.id);


    fetch('/Main/Upload', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data != null) {
                var fileContainer = document.createElement("div");
                fileContainer.classList.add("file_container");


                var imgElement = document.createElement("img");
                imgElement.classList.add("cross");
                imgElement.src = "/img/cross.png";
                imgElement.id = data.fileuploades.id;

                
                fileContainer.appendChild(imgElement);

               
                var linkElement = document.createElement("a");
                linkElement.target = "_blank";
                linkElement.href = "/Files/" + data.fileuploades.name;
                linkElement.classList.add("filecontainer");
                linkElement.textContent = data.fileuploades.name; 

                fileContainer.appendChild(linkElement);
                var Container = document.querySelector('.files_in_task');
                Container.appendChild(fileContainer);
                var remove = document.querySelectorAll('.cross');

                remove.forEach(function (cross) {
                    Remove(cross);
                });
            }
        })
});

var remove = document.querySelectorAll('.cross');

remove.forEach(function (cross) {
    Remove(cross);
});

function Remove(button) {
    button.addEventListener('click', function () {
        var parentDiv = button.closest('.file_container');
        fetch("/Main/RemoveFile", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(button.id)
        })
            .then(response => response.json())
            .then(data => {
                if (data) {
                    if (parentDiv) {
                        parentDiv.remove();
                    }
                }
            });
    });
}

