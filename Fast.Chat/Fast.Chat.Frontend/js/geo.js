var Geolocator = {
    setupGeolocation : function(){
        if ("geolocation" in navigator) {
            /* geolocation is available */
            navigator.geolocation.getCurrentPosition(function(position) {
                postGeolocation(position.coords);
              });
          } else {
            /* geolocation IS NOT available */

          }
    },

    postGeolocation : function(loc){
        $.ajax({
            type: "POST",
            url: "http://localhost:8080",
            data: "",
            headers:{
                'Content-Type':'application/json',
                'x-api-endpoint': 'SetLocation',
                'x-api-data': JSON.stringify({"latitude": loc.latitude, "longitude": loc.longitude})
            },
            success: function(data){
                console.log(JSON.parse(data));
            }
          });
    }
}