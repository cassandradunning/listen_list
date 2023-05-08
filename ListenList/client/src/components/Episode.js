import React from "react";
import { Card, CardBody } from "reactstrap";

const Episode = ({ episode }) => {
  return (
    <Card>
      <CardBody>
        <img
          src={episode.image}
          alt={episode.description}
          width="500"
          height="600"
        />
        <p>
          <strong>{episode.title}</strong>
        </p>
        <p>{episode.description}</p>
        <p>{episode.url}</p>
      </CardBody>
    </Card>
  );
};

export default Episode;
