$(() => {
    var stateSelectList = $('#State');
    var citySelectList = $('#City');

    populateStatesDropDown();    

    function populateStatesDropDown() {
        $.get(
            '/SelectedCityStatesAjax/GetStatesList', (states) => {

                for (var s = 0; s < states.length; s++) {
                    var opt = document.createElement('option');
                    opt.setAttribute('value', states[s]['Name']);
                    opt.appendChild(document.createTextNode(states[s]['Name']));

                    if (states[s]['Name'] === stateSelectList.attr('data-initial-val')) {
                        opt.setAttribute('selected', true);
                    }

                    stateSelectList.append(opt);
                }

                if (stateSelectList.val() !== '') {
                    populateCitiesDropDown();
                }
            }
        );
    }

    function populateCitiesDropDown() {
        $.get(
            `/SelectedCityStatesAjax/GetCitiesList?state=${stateSelectList.val()}`, (cities) => {

                for (var c = 0; c < cities.length; c++) {
                    var opt = document.createElement('option');
                    opt.setAttribute('value', cities[c]['City']);
                    opt.appendChild(document.createTextNode(cities[c]['City']));

                    if (cities[c]['City'] === citySelectList.attr('data-initial-val')) {
                        opt.setAttribute('selected', true);
                    }

                    citySelectList.append(opt);
                }
            }
        )
    }

    stateSelectList.on('change', (e) => {
        citySelectList.prop('options').length = 1;

        if (e.currentTarget.value !== '') {
            populateCitiesDropDown();
        }
    });
    
});