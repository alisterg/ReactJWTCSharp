import React, {Component} from 'react';
import {Alert} from "reactstrap";
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import {Map} from "./Map";
import {AddRemarkDialog} from "./AddRemarkDialog";

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);

    this.state = {
      isError: false,
      open: false,
      userLocation: {
        lat: 0,
        lng: 0
      },
      remarkText: ''
    };
  }
  
  handleOpen = () => {
    this.setState({open: true});
  };
  
  handleClose = () => {
    this.setState({open: false});
  };

  handleError = () => {
    this.setState({isError: true});
  };

  handleUserLocationChange = (userLocation) => {
    this.setState({userLocation: userLocation});
  };
  
  // handles a new remark submission
  handleSubmit = (remarkText) => {
    this.setState({remarkText: remarkText});
    
    let userData = localStorage.getItem('userData');
    userData = JSON.parse(userData);
    
    // the remark to save, username gets populated from the parsed auth header
    let postRemark = {
      note: remarkText,
      latitude: this.state.userLocation.lat,
      longitude: this.state.userLocation.lng
    };

    fetch('api/Landmark/AddRemark', {
      method: 'post',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + userData.token
      },
      body: JSON.stringify(postRemark)
    }).then(res => res.json())
      .then((res) => {
        this.setState({remarkText: ''});

        this.handleClose();
      });
  };

  render() {
    const err = (
      <Alert color="danger">
        This application required geolocation to work!
      </Alert>
    );

    const fab = (
      <Fab variant="extended" color="primary" onClick={this.handleOpen}>
        <AddIcon/>
        Add Remark
      </Fab>
    );
    
    const remarkDialog = this.state.open ? (
      <AddRemarkDialog
        onClose={this.handleClose}
        onSubmitted={this.handleSubmit}
      />
    ) : '';

    return (
      <div>
        <Map
          locationClickHandler={this.handleMyLocationPressed}
          remarks={this.props.remarks}
          onError={this.handleError}
          onUserLocationChange={this.handleUserLocationChange}
        />
        <div className="button-container">
          {this.state.isError ? err : fab}
        </div>
        
        {remarkDialog}
      </div>
    )
  }
}
