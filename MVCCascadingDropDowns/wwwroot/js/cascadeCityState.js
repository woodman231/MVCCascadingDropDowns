$(() => {

    var states = [];
    var cities = [];

    var stateSelectList = $('#State');
    var citySelectList = $('#City');

    var selectedState = stateSelectList.val();
    var selectedCity = citySelectList.val();

    populateStatesArray();
    populateCitiesArray();
    filterCities();

    function populateStatesArray() {
        stateSelectList.find('option').each((index, element) => {
            states.push(element.value);
        });
    }

    function populateCitiesArray() {
        citySelectList.find('option').each((index, element) => {
            cities.push({
                state: element.getAttribute('data-state'),
                city: element.value
            });
        });
    }

    function filterCities() {
        citySelectList.prop('options').length = 1;
        if (selectedState !== '') {
            for (var i = 0; i < cities.length; i++) {
                if (cities[i].state == selectedState) {
                    var selectThisItem = (cities[i].city === selectedCity);
                    var opt = document.createElement('option');
                    opt.setAttribute('data-state', cities[i].state);
                    opt.setAttribute('value', cities[i].city);
                    opt.appendChild(document.createTextNode(cities[i].city));

                    if (selectThisItem) {
                        opt.setAttribute('selected', true);
                    }

                    citySelectList.append(opt);

                }
            }
        }
    }

    stateSelectList.on('change', (e) => {
        selectedState = stateSelectList.val();

        filterCities();
    });

});