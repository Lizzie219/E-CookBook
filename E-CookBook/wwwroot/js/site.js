// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Checks whether the uploaded file is a photo
function validatePhoto(inputElement) {
    const file = inputElement.files[0];
    if (file) {
        const fileType = file['type'];
        const validImageTypes = ['image/jpeg', 'image/png', 'image/gif'];

        if (!validImageTypes.includes(fileType)) {
            alert('Please upload a valid image (.jpeg, .png, .gif).');
            inputElement.value = '';  // Clear the input
        }
    }
}

// Creates an input - Recipe Create and Edit, ingredients
function createInput(type = "text", nameAndID = "Default", ingredientCounter) {
    var input = document.createElement("input");
    input.type = type;
    input.name = nameAndID + "_" + ingredientCounter.toString();
    input.id = nameAndID + "_" + ingredientCounter.toString();

    return input;
}

// Handles Tag inputs - Recipe Create and Edit, tags
function tagInputKeyDown(e) {
    var tagInput = document.getElementById('tagInput');
    if (e.key === 'Enter' && tagInput.value.trim()) {
        addTag(tagInput.value.trim());
        tagInput.value = '';
    }
}

// Creates a tag from an input text and adds it to the input - Recipe Create and Edit, tags
function addTag(text) {
    var tagBadge = document.createElement("span");
    tagBadge.classList.add("badge");
    tagBadge.classList.add("rounded-pill");
    tagBadge.classList.add("text-bg-primary");
    tagBadge.classList.add("badge-background-color");
    tagBadge.innerText = text;

    var deleteButton = document.createElement("button");
    deleteButton.type = "button";
    deleteButton.classList += "btn-close";
    deleteButton.addEventListener('click', function () {
        var badge = this.closest('.badge');
        badge.parentNode.removeChild(badge);

        var tagsInput = document.getElementById("Tags");
        tagsInput.value = '';
        var allTags = document.getElementById("tagBadgeList").getElementsByTagName("span");
        for (var i = 0; i < allTags.length; i++) {
            tagsInput.value += (allTags[i].textContent + "|and|");
        }
    });

    tagBadge.appendChild(deleteButton);
    document.getElementById("tagBadgeList").appendChild(tagBadge);
    document.getElementById("Tags").value += (text + "|and|");
}

// Loads the basic ingredient values - Recipe Create and Edit, ingredients
function loadBaseIngredientValues(existingIngredient, ingredientCounter) {
    document.getElementById("IngredientName_" + ingredientCounter.toString()).value = existingIngredient.IngredientName;
    document.getElementById("Metric_" + ingredientCounter.toString()).value = existingIngredient.Metric;
    document.getElementById("MetricName_" + ingredientCounter.toString()).value = existingIngredient.MetricName;
}
