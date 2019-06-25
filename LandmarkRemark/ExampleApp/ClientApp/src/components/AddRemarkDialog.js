import {Component} from "react";
import React from "react";
import Dialog from "@material-ui/core/Dialog";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import TextField from "@material-ui/core/TextField";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import DialogTitle from "@material-ui/core/DialogTitle";

export class AddRemarkDialog extends Component {

  constructor(props) {
    super(props);

    this.state = {
      remarkText: ''
    };
  }
  
  handleClose = () => {
    this.props.onClose()
  };

  handleTextChange = remarkText => event => {
    this.setState({
      [remarkText]: event.target.value,
    });
  };

  handleSubmit = () => {
    this.props.onSubmitted(this.state.remarkText)
  };
  
  render() {
    return (
      <Dialog
        open={true}
        onClose={this.handleClose}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">Add Remark</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Please enter your remark. This will be visible to anyone
            using the app.
          </DialogContentText>
          <TextField
            autoFocus
            margin="dense"
            id="remarkText"
            label="Remark"
            fullWidth
            value={this.state.remarkText}
            onChange={this.handleTextChange("remarkText")}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={this.handleClose} color="primary">
            Cancel
          </Button>
          <Button onClick={this.handleSubmit} color="primary">
            Submit
          </Button>
        </DialogActions>
      </Dialog>
    );
  }
}