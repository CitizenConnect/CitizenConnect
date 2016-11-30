﻿//location service based lookup
//var geocoder = new google.maps.Geocoder;
//Citizens Connect's API
var API_KEY = 'AIzaSyA-UEiyhrkeVKo2UfwX3WNKqToGvgL9yFc';
var INFO_API = 'https://www.googleapis.com/civicinfo/v2/representatives';

// parsing out division IDs
var federal_pattern = "ocd-division/country:us";
var state_pattern = /ocd-division\/country:us\/state:(\D{2}$)/;
var cd_pattern = /ocd-division\/country:us\/state:(\D{2})\/cd:/;
var sl_pattern = /ocd-division\/country:us\/state:(\D{2})\/(sldl:|sldu:)/;
var county_pattern = /ocd-division\/country:us\/state:\D{2}\/county:\D+/;
var local_pattern = /ocd-division\/country:us\/state:\D{2}\/place:\D+/;
var district_pattern = /ocd-division\/country:us\/district:\D+/;

var federal_offices = ['United States Senate', 'United States House of Representatives']

var social_icon_lookup = {
    'YouTube': 'youtube',
    'Facebook': 'facebook',
    'Twitter': 'twitter',
    'GooglePlus': 'google-plus'
};

var social_link_lookup = {
    'YouTube': 'https://www.youtube.com/user/',
    'Facebook': 'https://www.facebook.com/',
    'Twitter': 'https://twitter.com/',
    'GooglePlus': 'https://plus.google.com/'
};

var selected_state = '';
var selected_county = '';
var selected_local = '';
var all_people = {};
var pseudo_id = 1;

function addressSearch() {
    var address = $('#address').val();
    //These lines correct the address line to avoid submitting invalid request see: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/encodeURIComponent
    //$.address.parameter('address', encodeURIComponent(address));  //original line from MyReps

    var params = {
        'key': API_KEY,
        'address': address
    }

    // API call (string is built from var defined above)
    $.when($.getJSON(INFO_API, params)).then(function (data) {
        var divisions = data['divisions'];
        var officials = data['officials'];
        var offices = data['offices'];

        $('table tbody').empty();

        selected_state = '';
        selected_county = '';
        selected_local = '';
        all_people = {};
        pseudo_id = 1;

        var federal_people = [];
        var state_people = [];
        var county_people = [];
        var local_people = [];

        console.log(local_people);
       

        //determines which container to display for each political division
        if (divisions === undefined) {
            $("#no-response-container").show();
            $("#response-container").hide();
        }
        else {
            setFoundDivisions(divisions);

            $.each(divisions, function (division_id, division) {
               
                if (typeof division.officeIndices !== 'undefined') {

                    $.each(division.officeIndices, function (i, office) {
                        var office_name = offices[office];

                        $.each(offices[office]['officialIndices'], function (i, official) {
                            var info = {
                                'person': null,
                                'office': office_name,
                                'address': null,
                                'channels': null,
                                'phones': null,
                                'urls': null,
                                'emails': null,
                                'division_id': division_id,
                                'pseudo_id': pseudo_id
                            };
                            
                            var person = officials[official];
                            info['person'] = person;

                            if (typeof person.channels !== 'undefined') {
                                var channels = [];
                                $.each(person.channels, function (i, channel) {
                                    if (channel.type != 'GooglePlus' && channel.type != 'YouTube') {
                                        channel['icon'] = social_icon_lookup[channel.type];
                                        channel['link'] = social_link_lookup[channel.type] + channel['id'];
                                        channels.push(channel);
                                    }
                                });
                                info['channels'] = channels;
                            }
                            if (typeof person.address !== 'undefined') {
                                info['address'] = person.address;
                            }
                            if (typeof person.phones !== 'undefined') {
                                info['phones'] = person.phones;
                            }
                            if (typeof person.urls !== 'undefined') {
                                info['urls'] = person.urls;
                            }
                            if (typeof person.emails !== 'undefined') {
                                info['emails'] = person.emails;
                            }

                            if (checkFederal(division_id, office_name)) {
                                info['jurisdiction'] = 'Federal Government';
                                federal_people.push(info);
                            } else if (checkState(division_id)) {
                                info['jurisdiction'] = selected_state;
                                state_people.push(info);
                            } else if (checkCounty(division_id)) {
                                info['jurisdiction'] = selected_county;
                                county_people.push(info);
                            } else {
                                info['jurisdiction'] = selected_local;
                                local_people.push(info);
                            }
                            all_people[pseudo_id] = info;
                            pseudo_id = pseudo_id + 1;

                        });

                    });
                }
            });

            $("#address-image").html("<img class='img-responsive img-thumbnail' src='https://maps.googleapis.com/maps/api/staticmap?size=600x200&maptype=roadmap&markers=" + encodeURIComponent(address) + "' alt='" + address + "' title='" + address + "' />");

            var template = new EJS({ 'text': $('#tableGuts').html() });

            $('#federal-results tbody').append(template.render({ people: federal_people }));

            if (state_people.length == 0)
                $('#state-container').hide();
            $('#state-results tbody').append(template.render({ people: state_people }));

            if (county_people.length == 0) {
                $('#county-container').hide();
                if (selected_county == '')
                    $('#county-container-not-found').hide();
                else
                    $('#county-container-not-found').show();
            }
            else {
                $('#county-container').show();
                $('#county-container-not-found').hide();
            }

            $('#county-results tbody').append(template.render({ people: county_people }));

            if (local_people.length == 0) {
                $('#local-container').hide();
                if (selected_local == '')
                    $('#local-container-not-found').hide();
                else
                    $('#local-container-not-found').show();
            }
            else {
                $('#local-container').show();
                $('#local-container-not-found').hide();
            }
            $('#local-results tbody').append(template.render({ people: local_people }));

            $('#response-container').show();
            $("#no-response-container").hide();

            // hook up modal stuff
            var modal_template = new EJS({ 'text': $('#modalGuts').html() });
            $('.btn-contact').off('click');
            $('.btn-contact').on('click', function () {
                var info = all_people[$(this).data('id')];
                $('#contactModalLabel').html("Contact " + info.person.name);
                $('#modalContent').html(modal_template.render({ info: info }));
                $('#contactModal').modal('show');
            })
        }
        //============== this pulls out the council ward number============== 
        local_people.sort(function(a, b) {
            return (a.office.name) - (b.office.name);
        });
        console.log(local_people[0].office.name);
        console.log(local_people[1].office.name);
        console.log(local_people);

        var ward = null;
        var councilPerson = null;
        var councilEmail = null;
        var councilPhone = null;
        var cityCouncilWebsite = null;

        if (local_people[0] == undefined) //does not contain local data then display not found message  
        {
            document.getElementById("WardNumber").innerHTML = "No Cleveland City Ward assignment found";
            document.getElementById("CouncilPerson").innerHTML = "";
            document.getElementById("CouncilEmail").innerHTML = "";
            document.getElementById("CouncilPhone").innerHTML = "";
            document.getElementById("CityCouncilWebsite").href = "http://www.clevelandcitycouncil.org/find-my-ward";
            document.getElementById("CityCouncilWebsite").innerHTML = "Find My Ward";
        }
        else
        {
            if (local_people[0].office.name.includes("Ward"))
            {
                var ward = (local_people[0].office.name);
                var councilPerson = (local_people[0].person.name);
                var councilEmail = (local_people[0].person.emails[0]);
                var councilPhone = (local_people[0].person.phones[0]);
                var cityCouncilWebsite = (local_people[0].urls[0]);
                document.getElementById("WardNumber").innerHTML = ward;
                document.getElementById("CouncilPerson").innerHTML = councilPerson;
                document.getElementById("CouncilEmail").innerHTML = councilEmail;
                document.getElementById("CouncilPhone").innerHTML = councilPhone;
                document.getElementById("CityCouncilWebsite").href = cityCouncilWebsite;
                document.getElementById("CityCouncilWebsite").innerHTML = "Ward Webpage";

            }
            else    //no ward data found
            {
                document.getElementById("WardNumber").innerHTML = "No Cleveland City Ward assignment found";
                document.getElementById("CouncilPerson").innerHTML = "";
                document.getElementById("CouncilEmail").innerHTML = "";
                document.getElementById("CouncilPhone").innerHTML = "";
                document.getElementById("CityCouncilWebsite").href = "http://www.clevelandcitycouncil.org/find-my-ward";
                document.getElementById("CityCouncilWebsite").innerHTML = "Find My Ward";
            }

        }

        return [local_people[0]];
        
    });
 

}

    // ========  GeoLocation =======
    //function findMe() {
    //    var foundLocation;
    //    if (navigator.geolocation) {
    //        navigator.geolocation.getCurrentPosition(function (position) {
    //            var latitude = position.coords.latitude;
    //            var longitude = position.coords.longitude;
    //            var accuracy = position.coords.accuracy;
    //            var coords = new google.maps.LatLng(latitude, longitude);

    //            console.log(coords);

    //            geocoder.geocode({
    //                'location': coords
    //            }, function (results, status) {
    //                if (status === google.maps.GeocoderStatus.OK) {
    //                    if (results[1]) {
    //                        $("#address").val(results[1].formatted_address);
    //                        addressSearch();
    //                    }
    //                }
    //            });

    //        }, function error(msg) {
    //            alert('Please enable your GPS position feature.');
    //        }, {
    //            //maximumAge: 600000,
    //            //timeout: 5000,
    //            enableHighAccuracy: true
    //        });
    //    } else {
    //        alert("Geolocation API is not supported in your browser.");
    //    }
    //};

    function setFoundDivisions(divisions) {

        // reset the labels
        $("#state-nav").hide();
        $("#county-nav").hide();
        $("#local-nav").hide();

        $.each(divisions, function (division_id, division) {
            if (state_pattern.test(division_id)) {
                selected_state = division.name;
                $("[id^=state-name]").html(selected_state);
                $("#state-nav").show();
            }
            if (county_pattern.test(division_id)) {
                selected_county = division.name;
                $("[id^=county-name]").html(selected_county);
                $("#county-nav").show();
            }
            if (local_pattern.test(division_id) || district_pattern.test(division_id)) {
                selected_local = division.name;
                $("[id^=local-name]").html(selected_local);
                $("#local-nav").show();
            }
        });
    }

    function checkFederal(division_id, office_name) {
        if (division_id == federal_pattern ||
            cd_pattern.test(division_id) ||
            federal_offices.indexOf(office_name.name) >= 0)
            return true;
        else
            return false;
    }

    function checkState(division_id) {
        if (state_pattern.test(division_id) ||
            sl_pattern.test(division_id))
            return true;
        else
            return false;
    }

    function checkCounty(division_id) {
        if (county_pattern.test(division_id))
            return true;
        else
            return false;
    }

    function formatParty(party) {
        if (party) {
            if (party == 'Unknown')
                return '';

            var party_letter = party.charAt(0);
            var css_class = 'label-ind';
            if (party_letter == 'D')
                css_class = 'label-dem';
            else if (party_letter == 'R')
                css_class = 'label-rep';

            return "(<span title='" + party + "' class='" + css_class + "'>" + party_letter + "</span>)";
        }
        else
            return '';
    }

    function toTitleCase(str) {
        return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
    }

    //converts a slug or query string in to readable text
    function convertToPlainString(text) {
        if (text === undefined) return '';
        return decodeURIComponent(text);
    }
  
