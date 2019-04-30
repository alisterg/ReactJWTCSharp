import {Component} from "react";
import {GoogleMap, Marker, withGoogleMap, withScriptjs} from "react-google-maps";
import React from "react";
import LocationIcon from '@material-ui/icons/MyLocation';
import Fab from "@material-ui/core/Fab";

export class Map extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isError: false,
      currentUserLocation: {
        lat: 0,
        lng: 0
      }
    };

    this.setMyLocation();
  }
  
  // gets the user's physical location
  setMyLocation = () => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        let pos = {
          lat: position.coords.latitude,
          lng: position.coords.longitude
        };

        this.setState({currentUserLocation: pos});
        this.props.onUserLocationChange(pos);
      }, () => {
        this.setState({isError: true})
      });
    } else {
      // Browser doesn't support Geolocation
      this.setState({isError: true})
    }
  };

  render() {
    let userData = localStorage.getItem('userData');
    userData = JSON.parse(userData);

    // show a marker for each remark
    const remarksToShow = this.props.remarks.map(remark =>
      <Marker 
        label={{color: (userData.username == remark.username) ? 'blue' : 'orange', text: remark.username}} 
        clickable={true}
        position={{lat: remark.latitude, lng: remark.longitude}} 
        key={remark.id}
      />
    );
    
    const MapComponent = withScriptjs(withGoogleMap((props) =>
      <GoogleMap
        defaultZoom={8}
        center={this.state.currentUserLocation}
      >
        {remarksToShow}
      </GoogleMap>
    ));

    return (
      <div style={{height: `400px`}}>
        <MapComponent
          googleMapURL="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places&key=AIzaSyB-s_Sb2b_WBeavGBAi1OhRwZ37K_WMrIs"
          loadingElement={<div style={{height: `100%`}}/>}
          containerElement={<div style={{height: `400px`}}/>}
          mapElement={<div style={{height: `100%`}}/>}
        />
        <div className="button-container-fab">
          <Fab variant="extended" onClick={this.setMyLocation}>
            <LocationIcon />
            My Location
          </Fab>
        </div>
      </div>
    );
  }
}