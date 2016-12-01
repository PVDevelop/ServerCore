import React from "react";
import { Link } from "react-router";
import Navbar from "react-bootstrap/lib/Navbar";
import Button from "react-bootstrap/lib/Button";
import ButtonGroup from "react-bootstrap/lib/ButtonGroup";

export default class Header extends React.Component {
    constructor(props) {
        super(props);

        this.onSignOutClicked = this.onSignOutClicked.bind(this);
        this.onSignInClicked = this.onSignInClicked.bind(this);
        this.onRegisterClicked = this.onRegisterClicked.bind(this);
    }

    render() {
        const isSigningInFirstTime = this.props.isSigningInFirstTime;
        const isSignedIn = this.props.isSignedIn;

        let content = null;
        if (isSigningInFirstTime) {
        }
        else if (isSignedIn) {
            content = (
                <ButtonGroup bsSize="small">
                    <Button onClick={this.onSignOutClicked}>
                        Выйти
                    </Button>
                </ButtonGroup >);
        }
        else {
            content =
                (
                    <ButtonGroup bsSize="small">
                        <Button onClick={this.onSignInClicked}>Войти</Button>
                        <Button onClick={this.onRegisterClicked}>Создать</Button >
                    </ButtonGroup >
                );
        }

        return (
            <Navbar fluid>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to="/">UCoach</Link>
                    </Navbar.Brand>
                </Navbar.Header>
                <Navbar.Collapse>
                    <Navbar.Form pullRight>
                        {content}
                    </Navbar.Form>
                </Navbar.Collapse>
            </Navbar>);
    }

    onSignOutClicked(e) {
        e.preventDefault();
        this.props.onSignOutClicked();
    }

    onSignInClicked(e) {
        e.preventDefault();
        this.props.onSignInClicked();
    }

    onRegisterClicked(e) {
        e.preventDefault();
        this.props.onRegisterClicked();
    }
}